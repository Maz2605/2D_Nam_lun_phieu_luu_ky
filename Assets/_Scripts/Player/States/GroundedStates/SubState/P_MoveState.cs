using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MoveState : P_GroundedState
{
    public P_MoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }
    

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Movement?.CheckIfShouldFlip(Player.InputManager.NormInputX);
        Movement?.SetVelocityX(PlayerData.moveSpeed * Player.InputManager.NormInputX * PlayerData.facingDirection);
        
        if (IsExitingState) return;
        if (Player.InputManager.NormInputX == 0)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Player.Anim.SetBool("Move", false);
    }
}
