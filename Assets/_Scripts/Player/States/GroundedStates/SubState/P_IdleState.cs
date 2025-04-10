using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_IdleState : P_GroundedState
{
    public P_IdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Movement?.SetVelocityX(0f);
        if (IsExitingState) return;
        if (InputManager.Instance.NormInputX != 0 )
        {
            StateMachine.ChangeState(Player.MoveState);
        }
        


    }
}
