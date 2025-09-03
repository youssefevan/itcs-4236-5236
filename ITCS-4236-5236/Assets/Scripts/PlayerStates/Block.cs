using UnityEngine;

public class Block : State
{
    public override void Enter()
    {
        Debug.Log("block");
        player.animator.Play("Block");
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

        if (!player.blockInput)
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
