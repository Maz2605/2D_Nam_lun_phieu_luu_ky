using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : BaseBullet
{
    public override void Effect(Collider2D collision)
    {
        base.Effect(collision);
        var dmg = collision.GetComponent<IDamageable>();
        if (dmg != null)
        {
            dmg.TakeDamage(bulletData.damage);
        }

        Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
            playerRb.AddForce(knockbackDir * bulletData.knockbackForce, ForceMode2D.Impulse);
        }
    }
}
