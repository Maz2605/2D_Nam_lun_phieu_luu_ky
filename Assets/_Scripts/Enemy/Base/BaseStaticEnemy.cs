using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BaseStaticEnemy : MonoBehaviour, IDamageable
{
    protected Rigidbody2D Rb { get; set; }
    private Collider2D Coll { get; set; }
    private Animator Anim { get; set; }
    protected Vector2 StartPos { get; set; }

    [SerializeField] private BaseStaticEnemyData staticEnemiesData;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform detectLine;
    [SerializeField] private Material blinkMaterial;
    private Material runtimeMaterial;
    private int materialID;

    protected enum State
    {
        Idle,
        Attack,
        Death,
    }

    protected State CurrentState = State.Idle;

    protected Transform target;

    private float _fireTimer = 0f;
    public int CurrentHealth { get; set; }
    public int faceDirection = -1;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
        runtimeMaterial = new Material(blinkMaterial);
        GetComponent<SpriteRenderer>().material = runtimeMaterial;
        materialID = Shader.PropertyToID("_BlinkStrength");
    }

    protected virtual void Start()
    {
        CurrentHealth = staticEnemiesData.health;
        StartPos = transform.position;
    }

    protected virtual void Update()
    {
        if (CurrentState == State.Death) return;

        DetectPlayer();
        if(CurrentState == State.Idle)
            Patrol();

        if (CurrentState == State.Attack)
        {
            Attack();
        }
    }

    public virtual void Patrol()
    {
        // Optional: Implement patrol logic for static enemies if needed
    }

    public virtual void Attack()
    {
        _fireTimer -= Time.deltaTime;
        if (_fireTimer <= 0f)
        {
            Shoot();
            _fireTimer = staticEnemiesData.attackCooldown;
        }
    }

    void DetectPlayer()
    {
        Vector2 origin = detectLine.position;
        Vector2 direction = Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction * faceDirection, staticEnemiesData.detectionRange,
            staticEnemiesData.playerMask);

        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            target = hit.collider.transform;
            CurrentState = State.Attack;
            Anim.SetBool("Attack", true);
        }
        else
        {
            target = null;
            Anim.SetBool("Attack", false);
            CurrentState = State.Idle;
        }

        Debug.DrawRay(origin, direction * faceDirection * staticEnemiesData.detectionRange, Color.red);
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("BulletPrefab or FirePoint is not assigned.");
            return;
        }

        GameObject bullet = PoolingManager.Instance.Spawn(bulletPrefab, firePoint.position, Quaternion.identity);

        if (bullet == null)
        {
            Debug.LogWarning("Bullet could not be spawned from PoolingManager.");
            return;
        }

        BaseBullet bulletScript = bullet.GetComponent<BaseBullet>();
        if (bulletScript == null)
        {
            Debug.LogWarning("Spawned bullet does not have BaseBullet component.");
            return;
        }

        bulletScript.SetDirectionFromEnemy(faceDirection);
        Debug.Log("Bullet spawned and direction set: " + faceDirection);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, staticEnemiesData.health);
        Debug.Log("Enemy: " + CurrentHealth);
        StartCoroutine(DamageEffect());
        if (CurrentHealth == 0)
            Dead();
    }

    IEnumerator DamageEffect()
    {
        DOTween.To(
                () => runtimeMaterial.GetFloat(materialID),
                x => runtimeMaterial.SetFloat(materialID, x),
                1f,
                0.1f
            )
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(() => runtimeMaterial.SetFloat(materialID, 0f));

        yield return new WaitForSeconds(0.25f);
    }

    public void Dead()
    {
        if (CurrentState == State.Death) return;

        CurrentState = State.Death;
        Coll.enabled = false;
        Destroy(gameObject, staticEnemiesData.destroyAfterSeconds);
    }
}
