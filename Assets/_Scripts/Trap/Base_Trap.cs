using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Trap : MonoBehaviour
{
    public int damage = 50;
    public float knockbackForce = 20;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Effect(other);
        }
    }

    public abstract void Effect(Collider2D other);
}
