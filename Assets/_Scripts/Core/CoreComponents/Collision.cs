using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : CoreComponent
{
    
    [Header("Ground Check")] [SerializeField]
    private Transform groundCheck;

    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    public Transform GroundCheck
    {
        get => GenericNotImplementError<Transform>.TryGet(groundCheck, transform.parent.name);
        private set => groundCheck = value;
    }
    public bool Ground
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);
    }
}