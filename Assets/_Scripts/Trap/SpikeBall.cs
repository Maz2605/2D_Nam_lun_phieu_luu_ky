using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    public int damage = 0;
    public float knockbackForce = 30f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var dg = other.gameObject.GetComponent<IDamageable>();
            var rb = other.gameObject.GetComponent<Rigidbody2D>();
            if (dg != null && rb != null)
            {
                Vector2 knockbackDir = (other.transform.position - transform.position).normalized;
                Vector2 adjustedKnockback = new Vector2(knockbackDir.x, knockbackDir.y).normalized;
                rb.velocity = Vector2.zero; 
                rb.AddForce(adjustedKnockback * knockbackForce, ForceMode2D.Impulse); 
                dg.TakeDamage(damage);
            }
        }
    }
}
