using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayerPoint : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetRespawnPosition(transform.position);
            animator.SetTrigger("Check");
        }
    }
}
