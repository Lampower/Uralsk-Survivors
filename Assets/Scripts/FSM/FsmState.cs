using UnityEngine;

public abstract class FsmState
{
    protected readonly Fsm fsm;
    public FsmState(Fsm fsm)
    {
        this.fsm = fsm;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();

}
