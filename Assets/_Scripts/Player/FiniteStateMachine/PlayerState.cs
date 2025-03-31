using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
        
    }
    public virtual void Enter()
    {
        DoCheck();
        //Anim
        StartTime = Time.time;
        //Debug.Log(_animName);
        IsAnimationFinished = false;
        IsExitingState = false;
    }

    public virtual void Execute()
    {
        
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
        //Anim
        IsExitingState = true;
    }

    public virtual void AnimationTrigger()
    {
        
    }
    
    public virtual void AnimationFinishedTrigger() => IsAnimationFinished = true;
    
}
