using UnityEngine;

public class LowBlock : Block
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

        if (!fighter.inputType.blockInput)
        {
            return states["crouch"];
        }

        if (!fighter.inputType.crouchInput)
        {
            return states["block"];
        }

        if (fighter.inputType.jumpInput)
        {
            return states["jumpsquat"];
        }

        return null;
    }

    public void BlockHit()
    {
        fighter.ApplyHitStop(fighter.incomingKBPower / 120f);

        Vector2 velocity = Vector2.zero;
        velocity = fighter.rb.linearVelocity;
        velocity.x = fighter.incomingKBAngle.x * fighter.incomingKBPower;
        fighter.rb.linearVelocity = velocity;
    }

}
