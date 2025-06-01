using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseEnemies : MonoBehaviour, IDamageable
{
    private Rigidbody2D Rb { get; set; }
    private Animator Anim { get; set; }
    private int FaceDirection { get; set; }

    public int CurrentHealth { get; set; }

    protected Vector2 startPos;
    private Transform target;
    protected float attackTimer = 0f;

    [SerializeField] protected BaseEnemiesData baseEnemiesData;

    public enum State
    {
        Patrol,
        Chasing,
        Dead
    }

    protected State CurrentState = State.Patrol;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>(); 
        CurrentHealth = baseEnemiesData.health;
    }

    private void Start()
    {
        startPos = transform.position;
        FaceDirection = baseEnemiesData.facingDirection;
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void FixedUpdate()
    {
        if (CurrentState == State.Dead) return;

        switch (CurrentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chasing:
                Attack();
                break;
        }
    }

    protected void Flip()
    {
        FaceDirection *= -1;
        Rb.transform.Rotate(0f, 180f, 0f);
    }

    protected void CheckIfShouldFlip()
    {
        if (FaceDirection == -1 && transform.position.x <= startPos.x - baseEnemiesData.patrolRange ||
            FaceDirection == 1 && transform.position.x >= startPos.x + baseEnemiesData.patrolRange)
            Flip();
    }

    public virtual void Patrol()
    {
        Rb.velocity = new Vector2(FaceDirection * baseEnemiesData.moveSpeed, Rb.velocity.y);
        CheckIfShouldFlip();
    }

    public virtual void Attack()
    {
        if (target == null)
        {
            CurrentState = State.Patrol;
            return;
        }

        float dir = Mathf.Sign(target.position.x - transform.position.x);
        Rb.velocity = new Vector2(dir * baseEnemiesData.moveSpeed, Rb.velocity.y);

        if ((dir > 0 && FaceDirection == -1) || (dir < 0 && FaceDirection == 1))
        {
            Flip();                                     
        }
    }

    void DetectPlayer()
    {
        attackTimer -= Time.deltaTime;
        Vector2 origin = transform.position;
        Vector2 direction = FaceDirection == 1 ? Vector2.right : Vector2.left;
        RaycastHit2D hit =
            Physics2D.Raycast(origin, direction, baseEnemiesData.detectRange, baseEnemiesData.playerMask);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            CurrentState = State.Chasing;
            target = hit.transform;
        }
        else
        {
            CurrentState = State.Patrol;
            target = null;
        }

        Debug.DrawRay(origin, direction * baseEnemiesData.detectRange, Color.red);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CurrentState == State.Dead) return;

        if (other.gameObject.CompareTag("Player") && attackTimer <= 0f)
        {
            AttackEffect(other);
        }
    }

    //Hiệu ứng và dame khi tấn công
    protected virtual void AttackEffect(Collision2D other)
    {
    }

    public virtual void Dead()
    {
        if (CurrentState == State.Dead) return;

        CurrentState = State.Dead;
        Rb.velocity = Vector2.zero;
        Rb.bodyType = RigidbodyType2D.Static;
        Destroy(gameObject, baseEnemiesData.destroyAfter);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, baseEnemiesData.health);
        Debug.Log(CurrentHealth);
        if (CurrentHealth == 0)
            Dead();
    }
}
