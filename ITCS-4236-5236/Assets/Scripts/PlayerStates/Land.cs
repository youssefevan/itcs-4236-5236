using Mono.Cecil.Cil;
using UnityEngine;
using System.Collections;

public class Land : State
{
    float currentTime = 0;
    float targetTime = 0.05f; // 3 frames at 60 fps

    public override void Enter()
    {
        Debug.Log("land");
        currentTime = 0;
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
            return states["idle"];
        }

        return null;
    }
}
