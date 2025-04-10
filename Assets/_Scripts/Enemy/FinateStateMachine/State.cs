using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    protected Core Core;
    protected Entity Entity;
    protected StateMachine StateMachine;
    
    public float StateStartTime { get; private set; }
    
    private string _animName;

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
    
    public State(Entity entity, StateMachine stateMachine, string animName)
    {
        Entity = entity;
        StateMachine = stateMachine;
        _animName = animName;
        Core = Entity.Core;
    }

    public virtual void DoCheck()
    {
        
    }
    public virtual void Enter()
    {
        StateStartTime = Time.time;
        Entity.Anim.SetBool(_animName, true);
        DoCheck();
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
        Entity.Anim.SetBool(_animName, false);
    }
}
