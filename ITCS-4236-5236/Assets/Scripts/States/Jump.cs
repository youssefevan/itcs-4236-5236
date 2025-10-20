using UnityEngine;

public class Jump : State
{
    Vector2 velocity;
    public override void Enter()
    {
        base.Enter();

        velocity = fighter.rb.linearVelocity;
        velocity.y += fighter.jumpForce;
        fighter.rb.linearVelocity = velocity;
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();
        fighter.FaceOpponent();

        velocity = fighter.rb.linearVelocity;

        velocity.x = Mathf.Lerp(
            velocity.x, fighter.inputType.moveInput * fighter.maxSpeed,
            fighter.airFriction * Time.fixedDeltaTime
        );

        velocity.y -= fighter.upGravity * Time.fixedDeltaTime;

        fighter.rb.linearVelocity = velocity;

        if (fighter.IsGrounded())
        {
            return states["idle"];
        }

        if (velocity.y < 0)
        {
            return states["fall"];
        }

        if (fighter.inputType.dodgeInput)
        {
            return states["dodge"];
        }

        return null;
    }
}
