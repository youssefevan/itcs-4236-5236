using UnityEngine;
using System.Collections.Generic;

public abstract class State
{
    protected StateManager manager;
    protected Dictionary<string, State> states;
    protected Fighter fighter;

    public void Init(StateManager sm, Dictionary<string, State> s, Fighter f)
    {
        manager = sm;
        states = s;
        fighter = f;
    }

    public virtual void Enter() { }

    public virtual State? PhysicsUpdate()
    {
        return null;
    }

    public virtual void Exit() { }
}
