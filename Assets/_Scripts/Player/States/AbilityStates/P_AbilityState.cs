using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AbilityStates : PlayerState
{
    protected bool IsAbilityDone;
    
    public P_AbilityStates(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Ability State");
        IsAbilityDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (IsAbilityDone)
        {
            if (IsGrounded && Movement?.CurVelocity.y <= 0.01f)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else
            {
                StateMachine.ChangeState(Player.InAirState);
            }
        }
    }
}
