using UnityEngine;

public class Idle : State
{
    public override void Enter()
    {
        Debug.Log("idle");
        fighter.animator.Play("Idle");
    }

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

        return handleTransitions();
    }

    State? handleTransitions()
    {
        if (!fighter.isGrounded())
        {
            return states["fall"];
        }

        if (fighter.moveInput != 0f)
        {
            return states["move"];
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
