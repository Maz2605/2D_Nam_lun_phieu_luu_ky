using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : CoreComponent
{
   [SerializeField] private Transform groundCheck;

   public Transform GroundCheck
   {
      get => GenericNotImplementError<Transform>.TryGet(groundCheck, transform.parent.name);
      private set => groundCheck = value;
   }
   
   [SerializeField] private float groundCheckRadius;
   [SerializeField] private LayerMask groundLayer;

   public float GroundCheckRadius
   {
      get => groundCheckRadius;
      set => groundCheckRadius = value;
   }
   
   public bool Ground
   {
      get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, groundLayer);
   }
}
