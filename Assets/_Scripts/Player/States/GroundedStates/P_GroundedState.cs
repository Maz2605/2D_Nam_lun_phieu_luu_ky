using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_GroundedState : PlayerState
{
    private bool _isGrounded;
    
    private Collision _collision;

    protected Collision Collision
    {
        get => _collision ? _collision : Core.GetCoreComponent(ref _collision);
    }
    
    private Movement _movement;

    protected Movement Movement
    {
        get => _movement ? _movement : Core.GetCoreComponent(ref _movement);
    }

    public P_GroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        
    }

    public override void DoCheck()
    {
        base.DoCheck();
        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        
        if (!IsExitingState)
        {
            if (InputManager.Instance.NormInputX != 0)
            {
                StateMachine.ChangeState(Player.MoveState);
            }
            else if (IsAnimationFinished)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }
    }
}
