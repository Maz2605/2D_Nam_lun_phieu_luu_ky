using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protect : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var dg = other.gameObject.GetComponent<IDamageable>();
            if (dg != null)
            {
                dg.TakeDamage(100);
            }
        }
    }
}
