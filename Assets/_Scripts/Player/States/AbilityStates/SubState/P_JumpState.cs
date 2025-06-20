using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class P_JumpState : P_AbilityStates
{
    private int _amountOfJumpLeft;
    public P_JumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
        _amountOfJumpLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlaySfxJump();
        Player.InputManager.SetJumpInputFalse();
        Movement?.SetVelocityY(PlayerData.jumpVelocity);
        IsAbilityDone = true;
        _amountOfJumpLeft--;
        // if (_amountOfJumpLeft == 0)
        // {
        //     Player.transform.DORotate(new Vector3(0, 0, -360f), 0.5f, RotateMode.FastBeyond360)
        //         .SetEase(Ease.Linear);
        // }
        Player.InAirState.SetIsJumpingTrue();
    }

    public bool CanJump() => _amountOfJumpLeft > 0;
    
    public void ResetJump() => _amountOfJumpLeft = PlayerData.amountOfJumps;
    
    public void DecreaseAmountOfJumpsLeft() => _amountOfJumpLeft--;

    
}
