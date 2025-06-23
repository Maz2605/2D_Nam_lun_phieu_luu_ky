using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : BaseEnemies
{
    [Header("Bat Settings")]
    public float detectRadius = 3f;
    public float attackSpeed = 7f;
    public float returnSpeed = 5f;

    private Vector2 _attackTargetPos;
    private bool _isReturning = false;

    protected override void Update()
    {
        if (CurrentState == State.Patrol)
            DetectPlayerByCircle();
    }

    protected override void Attack()
    {
        if (_isReturning)
        {
            Vector2 direction = (StartPos - (Vector2)transform.position).normalized;
            Rb.velocity = direction * returnSpeed;

            if (Vector2.Distance(transform.position, StartPos) < 0.1f)
            {
                Rb.velocity = Vector2.zero;
                CurrentState = State.Patrol;
                _isReturning = false;
            }
            return;
        }
        
        Vector2 dirToTarget = (_attackTargetPos - (Vector2)transform.position).normalized;
        if ((dirToTarget.x > 0 && faceDirection == -1) || (dirToTarget.x < 0 && faceDirection == 1))
        {
            Flip();
        }
        Rb.velocity = dirToTarget * attackSpeed;

        if (Vector2.Distance(transform.position, _attackTargetPos) < 0.2f)
        {
            _isReturning = true;
        }
    }

    private void DetectPlayerByCircle()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRadius, baseEnemiesData.playerMask);
        if (hit != null && hit.CompareTag("Player"))
        {
            CurrentState = State.Chasing;
            _attackTargetPos = hit.transform.position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

    protected override void AttackEffect(Collision2D other)
    {
        base.AttackEffect(other);
        if (other.gameObject.CompareTag("Player"))
        {
            var damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(baseEnemiesData.damage);
            }
            _isReturning = true; 
        }
    }
}
