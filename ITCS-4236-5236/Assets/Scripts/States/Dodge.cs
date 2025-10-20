using Mono.Cecil.Cil;
using UnityEngine;
using System.Collections;

public class Dodge : State
{
    float currentTime = 0;
    float targetTime = (15f / 60f); // 8 frames at 60 fps

    float targetDir = 0f;

    public override void Enter()
    {
        base.Enter();
        currentTime = 0;
        fighter.sprite.color = Color.cyan;
        fighter.collider.enabled = false;

        targetDir = fighter.inputType.moveInput;
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        currentTime += Time.fixedDeltaTime;

        Vector2 velocity = fighter.rb.linearVelocity;
        
        velocity.x = 1000.0f * targetDir * Time.fixedDeltaTime;
        velocity.y = 0f;

        fighter.rb.linearVelocity = velocity;

        if (currentTime >= targetTime)
        {
            velocity.x = 0f;
            fighter.rb.linearVelocity = velocity;
            if (fighter.IsGrounded())
            {
                return states["idle"];
            }
            else
            {
                return states["fall"];
            }
        }

        return null;
    }

    public override void Exit() {
        base.Exit();
        fighter.sprite.color = Color.white;
        fighter.collider.enabled = true;
    }
}
