using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTrap : MonoBehaviour
{
    public enum WindDirection { Up, Left, Right, Down }

    [Header("Wind Settings")]
    public WindDirection windDirection = WindDirection.Up;
    public float windForce = 10f;

    private Vector2 GetForceDirection()
    {
        return windDirection switch
        {
            WindDirection.Up => Vector2.up,
            WindDirection.Left => Vector2.left,
            WindDirection.Right => Vector2.right,
            WindDirection.Down => Vector2.down,
            _ => Vector2.up
        };
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb != null)
            {
                Vector2 forceDir = GetForceDirection();
                rb.AddForce(forceDir * windForce, ForceMode2D.Force);
            }
        }
    }
}

