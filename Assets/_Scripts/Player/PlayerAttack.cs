using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public float jumpForce = 10f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<IDamageable>();
            if (enemy != null)
            {
                AudioManager.Instance.PlaySfxHurt();
                enemy.TakeDamage(50);
                Rigidbody2D rb = transform.root.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
}