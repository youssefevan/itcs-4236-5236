using UnityEngine;

public class AttackGround : Attack
{
    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector2 velocity = fighter.rb.linearVelocity;

        if (velocity_type == 2) // addititve
        {
            velocity += velocity_modifier * Time.fixedDeltaTime;
        }
        else if (velocity_type == 3) // set (linear)
        {
            velocity = velocity_modifier * Time.fixedDeltaTime;
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
}
