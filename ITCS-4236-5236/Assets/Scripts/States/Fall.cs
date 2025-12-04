using UnityEngine;

public class Fall : State
{
    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();
        fighter.FaceOpponent();

        Vector2 velocity = fighter.rb.linearVelocity;

        velocity.x = Mathf.Lerp(
            velocity.x, fighter.inputType.moveInput * fighter.maxSpeed,
            fighter.airFriction * Time.fixedDeltaTime
        );

        velocity.y -= fighter.downGravity * Time.fixedDeltaTime;

        fighter.rb.linearVelocity = velocity;

        if (fighter.IsGrounded())
        {
            return states["land"];
        }

        if (fighter.inputType.dodgeInput && fighter.canDodge)
        {
            return states["dodge"];
        }

        return null;
    }

}
