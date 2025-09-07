using UnityEngine;

public class Move : State
{
    public override void Enter()
    {
        Debug.Log("move");
        fighter.animator.Play("Move");
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector2 velocity = fighter.rb.linearVelocity;

        velocity.x = Mathf.Lerp(
            velocity.x, fighter.inputType.moveInput * fighter.maxSpeed,
            fighter.groundFriction * Time.fixedDeltaTime
        );
        velocity.y = -1f;

        fighter.rb.linearVelocity = velocity;

        if (!fighter.IsGrounded())
        {
            return states["fall"];
        }

        if (fighter.inputType.moveInput == 0f)
        {
            return states["idle"];
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

        return null;
    }
}
