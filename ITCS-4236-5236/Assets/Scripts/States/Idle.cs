using UnityEngine;

public class Idle : State
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

        return handleTransitions();
    }

    State? handleTransitions()
    {
        if (!fighter.IsGrounded())
        {
            return states["fall"];
        }

        if (fighter.inputType.moveInput != 0f)
        {
            return states["move"];
        }

        if (fighter.inputType.jumpInput)
        {
            return states["jumpsquat"];
        }

        if (fighter.inputType.blockInput)
        {
            return states["block"];
        }

        if (fighter.inputType.crouchInput)
        {
            return states["crouch"];
        }

        if (fighter.inputType.kickInput)
        {
            return states["kickHigh"];
        }

        if (fighter.inputType.punchInput)
        {
            return states["punchHigh"];
        }

        return null;
    }

}
