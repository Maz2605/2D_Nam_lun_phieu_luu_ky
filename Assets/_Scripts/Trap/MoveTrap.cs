using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap : MonoBehaviour
{
    public enum MoveDirection { Up, Down, Left, Right }

    [Header("Movement Settings")]
    [SerializeField] private float moveDistance = 3f;             
    [SerializeField] private float moveSpeed = 2f;                 
    [SerializeField] private MoveDirection moveDirection = MoveDirection.Up;

    private Vector3 _startPos;
    private Vector3 _targetPos;
    private bool _forward = true;

    private void Start()
    {
        _startPos = transform.position;
        _targetPos = _startPos + GetDirectionVector() * moveDistance;
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

    private Vector3 GetDirectionVector()
    {
        return moveDirection switch
        {
            MoveDirection.Up => Vector3.up,
            MoveDirection.Down => Vector3.down,
            MoveDirection.Left => Vector3.left,
            MoveDirection.Right => Vector3.right,
            _ => Vector3.zero,
        };
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