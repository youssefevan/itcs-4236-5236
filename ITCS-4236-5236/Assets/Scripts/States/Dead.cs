using Mono.Cecil.Cil;
using UnityEngine;
using System.Collections;

public class Dead : State
{

    public override void Enter()
    {
        base.Enter();
        fighter.sprite.color = Color.red;
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

        return null;
    }
}
