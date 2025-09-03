using UnityEngine;

public class Idle : State
{
    public override void Enter()
    {
        Debug.Log("idle");
        player.animator.Play("Idle");
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector2 velocity = player.rb.linearVelocity;

        velocity.x = Mathf.Lerp(
            velocity.x, 0f,
            player.groundFriction * Time.fixedDeltaTime
        );
        velocity.y = -1f;

        player.rb.linearVelocity = velocity;

        if (!player.isGrounded())
        {
            return states["fall"];
        }

        if (player.hInput != 0f)
        {
            return states["move"];
        }

        if (player.jumpInput)
        {
            return states["jumpsquat"];
        }

        return null;
    }

}
