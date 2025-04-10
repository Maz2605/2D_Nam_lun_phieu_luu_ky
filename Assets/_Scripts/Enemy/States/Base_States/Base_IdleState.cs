using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_IdleState : State
{
    protected readonly Data_IdleState StateData;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInAgroRange;
    
    protected float idleTime;
    public Base_IdleState(Entity entity, StateMachine stateMachine, string animName, Data_IdleState stateData) : base(entity, stateMachine, animName)
    {
        StateData = stateData;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        // isPlayerInAgroRange = Entity.
    }
}
