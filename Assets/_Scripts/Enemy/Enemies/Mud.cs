using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : BaseEnemies
{
    protected override void AttackEffect(Collision2D other)
    {
        base.AttackEffect(other);
        Rigidbody2D playerRB = other.gameObject.GetComponent<Rigidbody2D>();
        var dg = other.gameObject.GetComponent<IDamageable>();
        if (playerRB != null)
        {
            dg.TakeDamage(baseEnemiesData.damage);
            Vector2 direction = (other.transform.position - transform.position).normalized;
            Vector2 knockback = new Vector2(direction.x, 0.5f).normalized * baseEnemiesData.knockbackForce;
            playerRB.AddForce(knockback, ForceMode2D.Impulse);
        }
        attackTimer = baseEnemiesData.attackCooldown;
        CurrentState = State.Patrol;
        Flip();
    }
}
