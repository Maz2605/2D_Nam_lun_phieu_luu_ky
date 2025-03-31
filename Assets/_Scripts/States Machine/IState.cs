public abstract class IState
{
    public abstract void Enter();
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void Exit();
    public abstract void DoChecks();
    public abstract void AnimationTrigger();
    public abstract void AnimationFinishedTrigger();
}

