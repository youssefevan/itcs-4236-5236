using UnityEngine;

public class Hitstun : State
{
    float currentTime = 0;
    float stunTime;

    public override void Enter()
    {
        base.Enter();
        stunTime = fighter.incomingStun;
        currentTime = 0;
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();
        currentTime += Time.fixedDeltaTime;

        Vector2 velocity = Vector2.zero;

        fighter.rb.linearVelocity = velocity;

        if (currentTime >= stunTime)
        {
            return states["knockback"];
        }

        return null;
    }
}
