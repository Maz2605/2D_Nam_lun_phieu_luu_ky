using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State _currentState;

    public void Initialize(State initialState)
    {
        _currentState = initialState;
        _currentState.Enter();
    }
    public void ChangeState(State newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void ExcutesState()
    {
        _currentState?.Execute();
    }
}
