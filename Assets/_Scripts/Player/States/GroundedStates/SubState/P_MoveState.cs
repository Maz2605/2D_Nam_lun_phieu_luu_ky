using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_MoveState : P_GroundedState
{
    public P_MoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("MoveState Enter");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Movement?.CheckIfShouldFlip(InputManager.Instance.NormInputX);
        Movement?.SetVelocityX(PlayerData.moveSpeed * InputManager.Instance.NormInputX);
        
        if (IsExitingState) return;
        if (InputManager.Instance.NormInputX == 0)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
    }
}
