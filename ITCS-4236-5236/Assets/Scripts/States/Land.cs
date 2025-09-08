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
        base.Enter();
        currentTime = 0;
        jumpQueued = false;
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        currentTime += Time.fixedDeltaTime;

        Vector2 velocity = fighter.rb.linearVelocity;

        velocity.x = Mathf.Lerp(
            velocity.x, 0f,
            fighter.groundFriction * Time.fixedDeltaTime
        );
        velocity.y = -1f;

        fighter.rb.linearVelocity = velocity;

        if (!fighter.IsGrounded())
        {
            return states["fall"];
        }

        if (currentTime >= targetTime)
        {
            if (jumpQueued)
            {
                return states["jumpsquat"];
            }
            else if (fighter.inputType.crouchInput)
            {
                return states["crouch"];
            }
            else
            {
                return states["idle"];
            }
        }

        // buffer jump
        if (fighter.inputType.jumpInput)
        {
            jumpQueued = true;
        }

        return null;
    }
}
