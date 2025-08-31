using UnityEngine;
using System.Collections.Generic;

public abstract class State
{
    protected StateManager manager;
    protected Dictionary<string, State> states;
    protected Player player;

    public void Init(StateManager sm, Dictionary<string, State> s, Player p)
    {
        manager = sm;
        states = s;
        player = p;
    }

    public virtual void Enter() {}

    public virtual State? PhysicsUpdate()
    {
        return null;
    }

    public virtual void Exit() {}
}
