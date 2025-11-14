using UnityEngine;

public class Crouch : State
{
    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();
        fighter.FaceOpponent();

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

        if (!fighter.inputType.crouchInput)
        {
            return states["idle"];
        }

        if (fighter.inputType.jumpInput)
        {
            return states["jumpsquat"];
        }

        if (fighter.inputType.blockInput)
        {
            return states["lowBlock"];
        }

        if (fighter.inputType.kickInput)
        {
            return states["kickLow"];
        }

        if (fighter.inputType.punchInput)
        {
            return states["punchLow"];
        }

        return null;
    }

}
