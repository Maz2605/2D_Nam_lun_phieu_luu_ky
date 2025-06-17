using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trunk : BaseStaticEnemy
{
    public int patrolRange = 5;
    public int moveSpeed = 2;
    
    public override void Patrol()
    {
        base.Patrol();
        Rb.velocity = new Vector2(FaceDirection * moveSpeed, Rb.velocity.y);
        if (FaceDirection == -1 && transform.position.x <= StartPos.x - patrolRange ||
            FaceDirection == 1 && transform.position.x >= StartPos.x + patrolRange)
            Flip();
        Vector2 wallCheckOrigin = transform.position + Vector3.right * FaceDirection * 0.5f;
        RaycastHit2D wallHit = Physics2D.Raycast(wallCheckOrigin, Vector2.right * FaceDirection, 0.1f, LayerMask.GetMask("Map"));
        if (wallHit.collider != null)
        {
            Flip();
        }
    }
    protected void Flip()
    {
        FaceDirection *= -1;
        Rb.transform.Rotate(0f, 180f, 0f);
    }
}
