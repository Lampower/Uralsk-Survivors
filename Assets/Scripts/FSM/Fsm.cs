using System;
using System.Collections.Generic;
using UnityEngine;

public class Fsm
{
    public FsmState CurrentState { get; private set; }

    private Dictionary<Type, FsmState> _states = new Dictionary<Type, FsmState>();
   
    public void AddState(FsmState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FsmState
    {
        var type = typeof(T);

        if (CurrentState?.GetType() == type)
            return;

        if (_states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
