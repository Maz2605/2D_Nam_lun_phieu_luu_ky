    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerBullet : MonoBehaviour, IDamageable
    {
        protected Vector2 direction;
        protected Vector2 startPos;
        
        [SerializeField] protected BaseBulletData bulletData;

        public virtual void SetDirection(Vector2 dir)
        {
            direction = dir.normalized;
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
            if (collision.CompareTag("Enemy"))
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
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            var dmg = collision.GetComponent<IDamageable>();
            if (dmg != null && collision.CompareTag("Enemy"))
            {
                dmg.TakeDamage(50); 
                if (rb != null)
                {
                    Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                    Vector2 adjustedKnockback = new Vector2(knockbackDir.x, 0.5f).normalized;
                    rb.velocity = Vector2.zero; 
                    rb.AddForce(adjustedKnockback * bulletData.knockbackForce, ForceMode2D.Impulse);
                }
            }
        }

        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }
    }
