using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageInterval = 1f;
    
    private Coroutine damageCoroutine;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var dg = other.GetComponent<IDamageable>();
            if (dg != null)
            {
                damageCoroutine = StartCoroutine(DealDamageOverTime(dg));
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop the coroutine if it is running
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }
    
    IEnumerator DealDamageOverTime(IDamageable player)
    {
        while (true)
        {
            player.TakeDamage(damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
