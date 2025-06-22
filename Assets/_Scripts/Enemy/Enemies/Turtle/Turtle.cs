using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    public int damage = 50;
    public float knockbackForce = 5f;
    public float safeDuration = 5f;
    public float growDuration = 1f;
    public float dangerousDuration = 5f;
    public float retractDuration = 1f;

    private enum TrapState { Safe = 0, Growing = 1, Dangerous = 2, Retracting = 3 }
    private TrapState currentState = TrapState.Safe;
    private float timer = 0f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        SetState(TrapState.Safe);
    }

    void Update()
    {
        timer += Time.deltaTime;

        switch (currentState)
        {
            case TrapState.Safe:
                if (timer >= safeDuration) SetState(TrapState.Growing);
                break;

            case TrapState.Growing:
                if (timer >= growDuration) SetState(TrapState.Dangerous);
                break;

            case TrapState.Dangerous:
                if (timer >= dangerousDuration) SetState(TrapState.Retracting);
                break;

            case TrapState.Retracting:
                if (timer >= retractDuration) SetState(TrapState.Safe);
                break;
        }
    }

    void SetState(TrapState newState)
    {
        currentState = newState;
        timer = 0f;
        if (anim != null)
            anim.SetInteger("State", (int)newState);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (IsDangerous() && other.gameObject.CompareTag("Player"))
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

    bool IsDangerous()
    {
        return currentState == TrapState.Growing ||
               currentState == TrapState.Dangerous ||
               currentState == TrapState.Retracting;
    }
}
