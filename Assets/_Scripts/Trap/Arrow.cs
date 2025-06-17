using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float force = 5f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var Rbb = other.gameObject.GetComponent<Rigidbody2D>();
            if (Rbb != null)
            {
                Rbb.AddForce(Vector2.up * force, ForceMode2D.Impulse);  
            }
        }
    }
}
