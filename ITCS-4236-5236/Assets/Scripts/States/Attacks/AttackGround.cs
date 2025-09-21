using System;
using UnityEngine;

public class AttackGround : Attack
{
    float direction = 1f;

    public override void Enter()
    {
        base.Enter();
        direction = fighter.facing;
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector2 velocity = fighter.rb.linearVelocity;

        if (modifyingVelocity)
        {
            if (velocity_type == 2) // addititve
            {
                velocity += velocity_modifier * new Vector2(direction, 1);
            }
            else if (velocity_type == 3) // set (linear)
            {
                velocity = velocity_modifier * new Vector2(direction, 1);
            }
        }
        else
        {
            velocity.x = Mathf.Lerp(
                velocity.x, 0f,
                fighter.groundFriction * Time.fixedDeltaTime
            );
            velocity.y = -1f;
        }

        if (!fighter.inHitStop)
        {
            fighter.rb.linearVelocity = velocity;
        }
        else
        {
            fighter.rb.linearVelocity = Vector2.zero;
        }

        return null;
    }

    public override void ApplyVelocity()
    {
        base.ApplyVelocity();
        Vector2 velocity = fighter.rb.linearVelocity;

        if (velocity_type == 1)
        {
            velocity = velocity_modifier * new Vector2(direction, 1);
        }

        fighter.rb.linearVelocity = velocity;
    }

}
