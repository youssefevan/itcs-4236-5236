using Mono.Cecil.Cil;
using UnityEngine;
using System.Collections;

public class Land : State
{
    float currentTime = 0;
    float targetTime = (4f / 60f); // 4 frames at 60 fps

    bool jumpQueued = false;

    public override void Enter()
    {
        Debug.Log("land");
        player.animator.Play("Land");
        currentTime = 0;
        jumpQueued = false;
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        currentTime += Time.fixedDeltaTime;

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

        if (currentTime >= targetTime)
        {
            if (jumpQueued)
            {
                return states["jumpsquat"];
            }
            else if (player.crouchInput)
            {
                return states["crouch"];
            }
            else
            {
                return states["idle"];
            }
        }

        // buffer jump
        if (player.jumpInput)
        {
            jumpQueued = true;
        }

        return null;
    }
}
