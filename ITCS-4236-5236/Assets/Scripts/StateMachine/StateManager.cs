using UnityEngine;
using System.Collections.Generic;

public class StateManager : MonoBehaviour
{
    public Dictionary<string, State> states = new Dictionary<string, State>();
    private State currentState;

    public void Init(Player player)
    {
        // this is bad. will clean up "eventually"
        State idle = new Idle();
        State fall = new Fall();
        State move = new Move();
        State jump = new Jump();
        State land = new Land();
        State jumpsquat = new JumpSquat();

        states.Add("idle", idle);
        states.Add("fall", fall);
        states.Add("move", move);
        states.Add("jump", jump);
        states.Add("land", land);
        states.Add("jumpsquat", jumpsquat);

        foreach (var state in states.Values)
        {
            state.Init(this, states, player);
        }

        currentState = states["idle"];
    }

    public void PhysicsUpdate()
    {
        var newState = currentState.PhysicsUpdate();

        if (newState != null)
        {
            ChangeState(newState);
        }

    }

    public void ChangeState(State newState)
    {
        currentState?.Exit();
        currentState = null;

        currentState = newState;
        currentState.Enter();
    }

    public State GetCurrentState()
    {
        return currentState;
    }

}
