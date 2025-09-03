using UnityEngine;

public class Move : State
{
    public override void Enter()
    {
        Debug.Log("move");
        player.animator.Play("Move");
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector2 velocity = player.rb.linearVelocity;

        velocity.x = Mathf.Lerp(
            velocity.x, player.hInput * player.maxSpeed,
            player.groundFriction * Time.fixedDeltaTime
        );
        velocity.y = -1f;

        player.rb.linearVelocity = velocity;

        if (!player.isGrounded())
        {
            return states["fall"];
        }

        if (player.hInput == 0f)
        {
            return states["idle"];
        }

        if (player.jumpInput)
        {
            return states["jumpsquat"];
        }

        return null;
    }
}
