using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct attackData
{
    [SerializeField] public int damage;
    [SerializeField] public int knockbackPower;
    [SerializeField] public Vector2 knockbackAngle;
    [SerializeField] public Vector2 velocityModifier;
    [SerializeField] public int velocityType;
    [SerializeField] public bool isLow;
}

public class StateManager : MonoBehaviour
{
    public Dictionary<string, State> states = new Dictionary<string, State>();
    private State currentState;

    // this is how I wrote this system, unfortunately
    [SerializeField]
    public attackData highKickData = new attackData
    {
        damage = 15,
        knockbackPower = 20,
        knockbackAngle = new Vector2(1, 1),
        velocityModifier = new Vector2(50, -1),
        velocityType = 1,
        isLow = false,
    };

    [SerializeField]
    public attackData highPunchData = new attackData
    {
        damage = 10,
        knockbackPower = 10,
        knockbackAngle = new Vector2(0.5f, 1),
        velocityModifier = new Vector2(15, -1),
        velocityType = 3,
        isLow = false,
    };
    [SerializeField]
    public attackData lowKickData = new attackData
    {
        damage = 10,
        knockbackPower = 20,
        knockbackAngle = new Vector2(0.5f, 1),
        velocityModifier = new Vector2(25, -1),
        velocityType = 1,
        isLow = true,
    };
    [SerializeField] public attackData lowPunchData = new attackData {
        damage = 10,
        knockbackPower = 10,
        knockbackAngle = new Vector2(1, 0.2f),
        velocityModifier = new Vector2(0, -1),
        velocityType = 0,
        isLow = true,
    };

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
        State lowBlock = new LowBlock();
        State crouch = new Crouch();
        State hitstun = new Hitstun();
        State knockback = new Knockback();
        State dodge = new Dodge();
        State dead = new Dead();

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
        states.Add("lowBlock", lowBlock);
        states.Add("hitstun", hitstun);
        states.Add("knockback", knockback);
        states.Add("dodge", dodge);
        states.Add("dead", dead);

        states.Add("kickHigh", kickHigh);
        states.Add("punchHigh", punchHigh);
        states.Add("kickLow", kickLow);
        states.Add("punchLow", punchLow);

        foreach (KeyValuePair<string, State> state in states)
        {
            state.Value.Init(this, states, fighter, state.Key);
        }

        kickHigh.SetAttackData(
            highKickData.damage,
            highKickData.knockbackPower,
            highKickData.knockbackAngle,
            highKickData.velocityModifier,
            highKickData.velocityType,
            highKickData.isLow
        );
        punchHigh.SetAttackData(
            highPunchData.damage,
            highPunchData.knockbackPower,
            highPunchData.knockbackAngle,
            highPunchData.velocityModifier,
            highPunchData.velocityType,
            highPunchData.isLow
        );
        kickLow.SetAttackData(
            lowKickData.damage,
            lowKickData.knockbackPower,
            lowKickData.knockbackAngle,
            lowKickData.velocityModifier,
            lowKickData.velocityType,
            lowKickData.isLow
        );
        punchLow.SetAttackData(
            lowPunchData.damage,
            lowPunchData.knockbackPower,
            lowPunchData.knockbackAngle,
            lowPunchData.velocityModifier,
            lowPunchData.velocityType,
            lowPunchData.isLow
        );

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
