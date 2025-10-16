using UnityEngine;
using System.Collections.Generic;

public class StateManager : MonoBehaviour
{
    public Dictionary<string, State> states = new Dictionary<string, State>();
    private State currentState;

    public void Init(Fighter fighter)
    {
        // this is bad. will clean up "eventually"
        State idle = new Idle();
        State fall = new Fall();
        State move = new Move();
        State jump = new Jump();
        State land = new Land();
        State jumpsquat = new Jumpsquat();
        State block = new Block();
        State crouch = new Crouch();
        State hitstun = new Hitstun();
        State knockback = new Knockback();

        AttackGround kickHigh = new AttackGround();
        AttackGround punchHigh = new AttackGround();
        AttackGround kickLow = new AttackGround();
        AttackGround punchLow = new AttackGround();

        states.Add("idle", idle);
        states.Add("fall", fall);
        states.Add("move", move);
        states.Add("jump", jump);
        states.Add("land", land);
        states.Add("jumpsquat", jumpsquat);
        states.Add("block", block);
        states.Add("crouch", crouch);
        states.Add("hitstun", hitstun);
        states.Add("knockback", knockback);

        states.Add("kickHigh", kickHigh);
        states.Add("punchHigh", punchHigh);
        states.Add("kickLow", kickLow);
        states.Add("punchLow", punchLow);

        foreach (KeyValuePair<string, State> state in states)
        {
            state.Value.Init(this, states, fighter, state.Key);
        }

        kickHigh.SetAttackData(15, 20, new Vector2(1, 1), new Vector2(50, -1), 1);
        punchHigh.SetAttackData(10, 10, new Vector2(1, 0.5f), new Vector2(15, -1), 3);
        kickLow.SetAttackData(10, 20, new Vector2(0.5f, 1), new Vector2(25, -1), 1);
        punchLow.SetAttackData(10, 10, new Vector2(1, 0.2f), new Vector2(0, -1), 0);

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
