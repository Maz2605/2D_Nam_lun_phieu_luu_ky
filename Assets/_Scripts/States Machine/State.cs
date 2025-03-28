using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : IState
{
    private StateMachine _stateMachine;

    public State(StateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }
    
    public virtual void Enter()
    {
        
    }
    
    public virtual void Execute()
    {
        
    }
    
    public virtual void Exit()
    {
        
    }

    public virtual void DoCheck()
    {
        
    }

    public virtual void AnimationTrigger()
    {
        
    }

    public virtual void AnimationFinishedTrigger()
    {
        
    }
}
