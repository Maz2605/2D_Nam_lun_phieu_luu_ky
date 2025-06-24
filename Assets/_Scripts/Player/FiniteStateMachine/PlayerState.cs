using UnityEngine;

public class PlayerState
{
    protected Core Core;
    protected Player Player;
    protected PlayerStateMachine StateMachine;
    protected PlayerData PlayerData;
    
    protected float StartTime;
    
    protected bool IsAnimationFinished;
    protected bool IsExitingState;

    //Get Component
    private Collision _collision;

    protected Collision Collision
    {
        get => _collision ? _collision : Core.GetCoreComponent<Collision>();
    }
    
    private Movement _movement;

    protected Movement Movement
    {
        get => _movement ? _movement : Core.GetCoreComponent<Movement>();
    }
    
    //Check 
    protected bool IsGrounded;
    
    private string _animName;
    
    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName)
    {
        Player = player;
        StateMachine = stateMachine;
        PlayerData = playerData;
        _animName = animName;
        Core = player.Core;
    }

    public virtual void DoCheck()
    {
        if (Collision)
        {
            IsGrounded = Collision.Ground;
        }
    }
    public virtual void Enter()
    {
        DoCheck();
        Player.Anim.SetBool(_animName, true);
        StartTime = Time.time;
        IsAnimationFinished = false;
        IsExitingState = false;
    }
    
    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }

    public virtual void Exit()
    {
        Player.Anim.SetBool(_animName, false);
        IsExitingState = true;
    }

    
    
}
