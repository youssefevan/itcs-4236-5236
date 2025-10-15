using UnityEngine;
using System.Collections.Generic;

public abstract class State
{
    protected StateManager manager;
    protected Dictionary<string, State> states;
    protected Fighter fighter;
    protected string name;

    public void Init(StateManager sm, Dictionary<string, State> s, Fighter f, string n)
    {
        manager = sm;
        states = s;
        fighter = f;
        name = n;
    }

    public virtual void Enter()
    {
        fighter.animator.Play(name);
        fighter.attackCompleted = false;
        //Debug.Log(fighter + ": " + name);
    }

    public virtual State? PhysicsUpdate()
    {
        if (fighter.inHitStop)
        {
            return null;
        }
        return null;
    }

    public virtual void Exit() { }
}
