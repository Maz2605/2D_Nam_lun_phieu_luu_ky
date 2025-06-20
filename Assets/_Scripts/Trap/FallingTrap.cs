using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    private Rigidbody2D Rb;
    private bool triggered = false;
    
    public float timeWait = 2.0f;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.bodyType = RigidbodyType2D.Kinematic; 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(TriggerFall());
        }
    }

   IEnumerator TriggerFall()
   {
       yield return new WaitForSeconds(timeWait);
        Rb.bodyType = RigidbodyType2D.Dynamic; 
        Rb.velocity = new Vector2(0, 2f);
        yield return new WaitForSeconds(3f); 
        // gameObject.SetActive(false); 
    }
}
