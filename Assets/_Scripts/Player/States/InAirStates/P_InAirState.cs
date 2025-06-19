using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_InAirState : PlayerState
{
    //Check
    private bool _isJumping;
    private bool _coyoteTime;

    public P_InAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(
        player, stateMachine, playerData, animName)
    {
    }

  
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckJumpMultipler();
        Player.MaxFallSpeed();

        if (IsGrounded && Movement.CurVelocity.y <= 0.01f)
        {
            StateMachine.ChangeState(Player.GroundedState);
        }
        else if (Player.InputManager.JumpInput && Player.JumpState.CanJump())
        {
            StateMachine.ChangeState(Player.JumpState);
        }
        else
        {
            Movement?.CheckIfShouldFlip(Player.InputManager.NormInputX);
            Movement?.SetVelocityX(PlayerData.moveSpeed * Player.InputManager.NormInputX * PlayerData.facingDirection);
        }
    }


    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > StartTime + PlayerData.coyoteTime)
        {
            _coyoteTime = false;
            Player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime()
    {
        _coyoteTime = true;
    }

    private void CheckJumpMultipler()
    {
        if (_isJumping)
        {
            if (Player.InputManager.JumpInputStop)
            {
                Movement?.SetVelocityY(Movement.CurVelocity.y * PlayerData.jumpHeightMultiplier);
                _isJumping = false;
            }
            else if (Movement?.CurVelocity.y <= 0.01f)
            {
                _isJumping = false;
            }
        }
    }

    
    public void SetIsJumpingTrue() => _isJumping = true;
    
}