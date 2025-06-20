using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_GroundedState : PlayerState
{
    
    public P_GroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) :
        base(player, stateMachine, playerData, animName)
    {
    }
    

    public override void Enter()
    {
        base.Enter();
        Player.JumpState.ResetJump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Player.InputManager.JumpInput && Player.JumpState.CanJump())
        {
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!IsGrounded)
        {
            Player.InAirState.StartCoyoteTime();
            StateMachine.ChangeState(Player.InAirState);
        }
        
        if (!IsExitingState)
        {
            if (Player.InputManager.NormInputX != 0)
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