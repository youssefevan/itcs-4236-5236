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

        AttackGround kickGroundNeutral = new AttackGround();
        AttackAir kickAirDown = new AttackAir();
        AttackGround punchGroundNeutral = new AttackGround();

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

        states.Add("kickGroundNeutral", kickGroundNeutral);
        states.Add("kickAirDown", kickAirDown);
        states.Add("punchGroundNeutral", punchGroundNeutral);

        foreach (KeyValuePair<string, State> state in states)
        {
            state.Value.Init(this, states, fighter, state.Key);
        }

        kickGroundNeutral.SetAttackData(10, 10, new Vector2(1, 1), new Vector2(50, -1), 1);
        kickAirDown.SetAttackData(10, 20, new Vector2(1, -1), new Vector2(15, -15), 3);
        punchGroundNeutral.SetAttackData(10, 20, new Vector2(1, 1), new Vector2(30, -1), 3);

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
