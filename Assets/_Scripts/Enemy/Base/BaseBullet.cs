using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class    BaseBullet : MonoBehaviour
{
    protected Vector2 direction;
    protected Vector2 startPos;

    [SerializeField] protected BaseBulletData bulletData;

    public virtual void SetDirectionFromEnemy(int faceDirection)
    {
        direction = new Vector2(faceDirection, 0).normalized;
        startPos = transform.position;
    }

    protected virtual void Update()
    {
        transform.Translate(direction * bulletData.speed * Time.deltaTime);

        if (Vector2.Distance(startPos, transform.position) >= bulletData.maxDistance)
        {
            PoolingManager.Instance.Despawn(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Effect(collision);
            PoolingManager.Instance.Despawn(gameObject);
        }
        else if (!collision.CompareTag("Enemy"))
        {
            PoolingManager.Instance.Despawn(gameObject);
        }
    }

    public virtual void Effect(Collider2D collision)
    {
        Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
        var dmg = collision.GetComponent<IDamageable>();
        if (dmg != null)
        {
            dmg.TakeDamage(bulletData.damage);
            if (playerRb != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                Vector2 adjustedKnockback = new Vector2(knockbackDir.x, 0.5f).normalized;
                playerRb.velocity = Vector2.zero;
                playerRb.AddForce(adjustedKnockback * bulletData.knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}

