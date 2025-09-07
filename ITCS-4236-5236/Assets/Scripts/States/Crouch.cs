using UnityEngine;

public class Crouch : State
{
    public override void Enter()
    {
        Debug.Log("crouch");
        fighter.animator.Play("Crouch");
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

        if (!fighter.isGrounded())
        {
            return states["fall"];
        }

        if (!fighter.crouchInput)
        {
            return states["idle"];
        }

        if (fighter.jumpInput)
        {
            return states["jumpsquat"];
        }

        return null;
    }

}
