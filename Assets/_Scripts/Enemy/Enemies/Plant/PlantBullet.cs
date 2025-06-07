using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : BaseBullet
{
    public override void Effect(Collider2D collision)
    {
        base.Effect(collision);
        Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
        var dmg = collision.GetComponent<IDamageable>();
        if (dmg != null && collision.CompareTag("Player"))
        {
            dmg.TakeDamage(bulletData.damage);
            if (playerRb != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;

                // Giữ hướng X mạnh hơn và thêm lực Y để đẩy player lên một chút
                Vector2 adjustedKnockback = new Vector2(knockbackDir.x, 0.5f).normalized;

                playerRb.velocity = Vector2.zero; // Reset velocity trước khi đẩy
                playerRb.AddForce(adjustedKnockback * bulletData.knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
