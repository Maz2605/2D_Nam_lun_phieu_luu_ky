using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var dg = other.GetComponent<IDamageable>();
            if (dg != null)
            {
                dg.TakeDamage(100);
            }
        }
    }
}
