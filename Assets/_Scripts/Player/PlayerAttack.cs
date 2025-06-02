using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator Anim { get; private set; }
    public float jumpForce = 10f;

    private void Start()
    {
        Anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<IDamageable>();
            if (enemy != null)
            {
                enemy.TakeDamage(50);
                Rigidbody2D rb = transform.root.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
}