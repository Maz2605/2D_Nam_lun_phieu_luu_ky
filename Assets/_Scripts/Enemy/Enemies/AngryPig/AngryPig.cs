using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPig : BaseEnemies
{
    public override void Attack()
    {
        MoveSpeed = baseEnemiesData.moveSpeed * 2.5f;
        base.Attack();
    }

    protected override void AttackEffect(Collision2D other)
    {
        base.AttackEffect(other);
        
        Rigidbody2D playerRB = other.gameObject.GetComponent<Rigidbody2D>();
        var dg = other.gameObject.GetComponent<IDamageable>();
        if (playerRB != null && other.gameObject.CompareTag("Player"))
        {
            dg.TakeDamage(baseEnemiesData.damage);
            Vector2 direction = (other.transform.position - transform.position).normalized;
            Vector2 knockback = new Vector2(direction.x, 0.2f).normalized * baseEnemiesData.knockbackForce;
            playerRB.AddForce(knockback, ForceMode2D.Impulse);
        }
        AttackTimer = baseEnemiesData.attackCooldown;
        CurrentState = State.Patrol;
        Flip();
    }
}
