using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rino : BaseEnemies
{
    public override void Patrol()
    {
        Rb.velocity = Vector2.zero;
    }

    public override void Attack()
    {
        Rb.velocity = FaceDirection == 1 ? Vector2.right * MoveSpeed : Vector2.left * MoveSpeed;
        if (IsWallAhead())
        {
            Target = null;
            CurrentState = State.Patrol;
            Flip();
        }
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
    }
}
