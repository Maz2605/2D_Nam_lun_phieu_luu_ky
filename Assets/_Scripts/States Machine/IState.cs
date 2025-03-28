using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();
    public void Execute();
    public void Exit();
    public void DoCheck();
    public void AnimationTrigger();
    public void AnimationFinishedTrigger();
}
