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
            velocity.x, fighter.moveInput * fighter.maxSpeed,
            fighter.groundFriction * Time.fixedDeltaTime
        );
        velocity.y = -1f;

        fighter.rb.linearVelocity = velocity;

        if (!fighter.isGrounded())
        {
            return states["fall"];
        }

        if (fighter.moveInput == 0f)
        {
            return states["idle"];
        }

        if (fighter.jumpInput)
        {
            return states["jumpsquat"];
        }

        if (fighter.blockInput)
        {
            return states["block"];
        }

        if (fighter.crouchInput)
        {
            return states["crouch"];
        }

        return null;
    }
}
