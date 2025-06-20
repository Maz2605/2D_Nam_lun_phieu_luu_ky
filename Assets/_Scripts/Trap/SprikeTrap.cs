using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprikeTrap : Base_Trap
{
    
    public override void Effect(Collider2D other)
    {
        Rigidbody2D playerRB = other.gameObject.GetComponent<Rigidbody2D>();
        var dg = other.gameObject.GetComponent<IDamageable>();
        if (playerRB != null && other.gameObject.CompareTag("Player"))
        {
            dg.TakeDamage(damage);
            Vector2 direction = (other.transform.position - transform.position).normalized;
            Vector2 knockback = new Vector2(direction.x, 0.2f).normalized * knockbackForce; 
            playerRB.AddForce(knockback, ForceMode2D.Impulse);
        }
    }
}
