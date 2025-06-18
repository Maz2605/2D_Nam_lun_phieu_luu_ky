using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveDistance = 3f;      // Khoảng cách di chuyển từ StartPos
    [SerializeField] private float moveSpeed = 2f;         // Tốc độ di chuyển
    [SerializeField] private bool vertical = true;         // Di chuyển theo chiều dọc hay ngang
    
    private Vector3 _startPos;
    private Vector3 _targetPos;
    private bool _forward = true;

    private void Start()
    {
        _startPos = transform.position;
        _targetPos = _startPos + (vertical ? Vector3.up : Vector3.right) * moveDistance;
    }

    private void Update()
    {
        Vector3 target = _forward ? _targetPos : _startPos;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            _forward = !_forward;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
