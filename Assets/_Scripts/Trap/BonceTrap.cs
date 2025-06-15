using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonceTrap : MonoBehaviour
{
    public float initialBounceForce = 10f;
    public float bounceIncreasePerUse = 1f;
    public float maxBounceForce = 25f;
    public float resetTime = 3f;  

    private float currentBounceForce;
    private float timeSinceLastUse = 0f;
    private Animator anim;

    void Start()
    {
        currentBounceForce = initialBounceForce;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timeSinceLastUse += Time.deltaTime;
        if (timeSinceLastUse >= resetTime)
        {
            currentBounceForce = initialBounceForce;
        }
        
        if (anim != null && timeSinceLastUse >= 1.5f)
        {
            anim.SetBool("Active", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * currentBounceForce, ForceMode2D.Impulse);
                
                currentBounceForce += bounceIncreasePerUse;
                currentBounceForce = Mathf.Min(currentBounceForce, maxBounceForce);
                
                timeSinceLastUse = 0f;
                if (anim != null)
                    anim.SetBool("Active", true);
            }
        }
    }
}
