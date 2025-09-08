using UnityEngine;

public class Block : State
{
    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector2 velocity = fighter.rb.linearVelocity;

        velocity.x = Mathf.Lerp(
            velocity.x, 0f,
            fighter.groundFriction * Time.fixedDeltaTime
        );
        velocity.y = -1f;

        fighter.rb.linearVelocity = velocity;

        if (!fighter.IsGrounded())
        {
            return states["fall"];
        }

        if (!fighter.inputType.blockInput)
        {
            return states["idle"];
        }

        if (fighter.inputType.jumpInput)
        {
            return states["jumpsquat"];
        }

        return null;
    }

}
