using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStaticEnemy : MonoBehaviour, IDamageable
{
     private Rigidbody2D Rb { get; set; }
    private Animator Anim { get; set; }
    
    [SerializeField] private BaseStaticEnemyData staticEnemiesData;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    
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
    public int FaceDirection { get; set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        CurrentHealth = staticEnemiesData.health;
        FaceDirection = staticEnemiesData.facingDirection;
    }

    private void Start()
    {
        FaceDirection = staticEnemiesData.facingDirection;
    }

    private void Update()
    {
        if(CurrentState == State.Death) return;

        DetectPlayer();
        
        if (CurrentState == State.Attack)
        {
            Attack();
        }
    }
    

    public void Patrol()
    {
        
    }

    public void Attack()
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
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction * FaceDirection, staticEnemiesData.detectionRange, staticEnemiesData.playerMask );

        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            target = hit.collider.transform;
            CurrentState = State.Attack;
        }
        else
        {
            target = null;
            CurrentState = State.Idle;
        }
        Debug.DrawRay(origin, direction * FaceDirection * staticEnemiesData.detectionRange, Color.red);
    }
    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = PoolingManager.Instance.Spawn(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector2 shootDir = firePoint.right.normalized;
        bullet.GetComponent<BaseBullet>().SetDirection(shootDir * FaceDirection);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(damage, 0, staticEnemiesData.health);
    }

    public void Dead()
    {
        if(CurrentState == State.Death) return;
        
        CurrentState = State.Death;
        Rb.velocity = Vector2.zero;
        Rb.bodyType = RigidbodyType2D.Static;
        Destroy(gameObject, staticEnemiesData.destroyAfterSeconds);
    }
}
