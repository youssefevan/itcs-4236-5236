using UnityEngine;

public class Jump : State
{
    Vector2 velocity;
    public override void Enter()
    {
        Debug.Log("jump");

        velocity = player.rb.linearVelocity;
        velocity.y += player.jumpForce;
        player.rb.linearVelocity = velocity;
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        velocity = player.rb.linearVelocity;

        velocity.x = Mathf.Lerp(
            velocity.x, player.hInput * player.maxSpeed,
            player.airFriction * Time.fixedDeltaTime
        );

       velocity.y -= player.upGravity * Time.fixedDeltaTime;

        player.rb.linearVelocity = velocity;

        if (player.isGrounded())
        {
            return states["idle"];
        }

        if (velocity.y < 0)
        {
            return states["fall"];
        }

        return null;
    }
}
