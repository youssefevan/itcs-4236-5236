using UnityEngine;

public class Fall : State
{

    public override void Enter()
    {
        Debug.Log("fall");
        fighter.animator.Play("Fall");
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

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

        return null;
    }

}
