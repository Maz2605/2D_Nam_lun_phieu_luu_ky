using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : BaseStaticEnemy
{
    public int facingDirection = -1;

    protected override void Start()
    {
        base.Start();
        FaceDirection = facingDirection;
    }
}
