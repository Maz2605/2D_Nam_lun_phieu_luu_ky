using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        
    }
}
