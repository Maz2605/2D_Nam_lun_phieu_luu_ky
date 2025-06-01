using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Effect(other.gameObject.GetComponent<Player>());
        }
            
    }

    public abstract void Effect(Player player);
}
