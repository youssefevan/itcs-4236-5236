using UnityEngine;

public class AttackGround : Attack
{
    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector2 velocity = fighter.rb.linearVelocity;

        if (modifyingVelocity)
        {
            if (velocity_type == 2) // addititve
            {
                velocity += velocity_modifier;
            }
            else if (velocity_type == 3) // set (linear)
            {
                velocity = velocity_modifier;
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

        fighter.rb.linearVelocity = velocity;

        return null;
    }

    public override void ApplyVelocity()
    {
        base.ApplyVelocity();
        Vector2 velocity = fighter.rb.linearVelocity;

        if (velocity_type == 1)
        {
            velocity = velocity_modifier * fighter.facing;
        }

        fighter.rb.linearVelocity = velocity;
    }

}
