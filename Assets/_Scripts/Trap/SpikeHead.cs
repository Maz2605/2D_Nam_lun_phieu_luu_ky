using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 3f; // khoảng cách mỗi cạnh
    public float waitTime = 0.2f;   // thời gian chờ nhỏ giữa các hướng

    private Vector2[] directions = new Vector2[]
    {
        Vector2.right,   // phải
        Vector2.up,      // lên
        Vector2.left,    // trái
        Vector2.down     // xuống
    };

    private int currentDirectionIndex = 0;
    private Vector3 startPos;
    private Vector3 targetPos;
    private Animator animator;
    private bool isMoving = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetNextTarget();
    }

    private void Update()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            isMoving = false;
            StartCoroutine(WaitAndMove());
        }
    }

    private IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(waitTime);

        currentDirectionIndex = (currentDirectionIndex + 1) % directions.Length;
        SetNextTarget();
        isMoving = true;
    }

    private void SetNextTarget()
    {
        startPos = transform.position;
        Vector2 dir = directions[currentDirectionIndex];
        targetPos = startPos + (Vector3)(dir * moveDistance);
        PlayAnimation(dir);
    }

    private void PlayAnimation(Vector2 dir)
    {
        if (animator == null) return;

        if (dir == Vector2.right)
            animator.Play("MoveRight");
        else if (dir == Vector2.left)
            animator.Play("MoveLeft");
        else if (dir == Vector2.up)
            animator.Play("MoveUp");
        else if (dir == Vector2.down)
            animator.Play("MoveDown");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var dg = other.gameObject.GetComponent<IDamageable>();
            dg.TakeDamage(100);
            
        }
    }
}
