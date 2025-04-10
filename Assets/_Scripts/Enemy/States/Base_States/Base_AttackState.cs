using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_AttackState : State
{
    protected Transform AttackTransform;
    
    protected bool IsAnimationFinished;
    protected bool IsPlayerInAgroRange;

    public Base_AttackState(Entity entity, StateMachine stateMachine, string animName, Transform attackTransform) : base(entity, stateMachine, animName)
    {
        AttackTransform = attackTransform;
    }

    public override void DoCheck()
    {
        base.DoCheck();

        IsPlayerInAgroRange = Entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        Entity.AnimationToStateMachine.attackState = this;
        IsAnimationFinished = false;
        Movement?.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityX(0f);
    }

    public virtual void TriggerAttack()
    {
        
    }
    public virtual void FinishAttack()
    {
        IsAnimationFinished = true;
    }
}
