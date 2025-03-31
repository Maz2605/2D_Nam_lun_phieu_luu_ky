using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class StateMachine
{
    private IState _currentState;
    
    public IState CurrentState
    {
        get => _currentState;
        set => _currentState = value;
    }

    public void Initialize(IState initialState)
    {
        _currentState = initialState;
        _currentState.Enter();
    }
    public void ChangeState(IState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void UpdateState()
    {
        _currentState?.LogicUpdate();
        _currentState?.PhysicsUpdate();
    }
}
