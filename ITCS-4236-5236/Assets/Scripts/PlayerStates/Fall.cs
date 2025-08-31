using UnityEngine;

public class Fall : State
{

    public override void Enter()
    {
        Debug.Log("fall");
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector2 velocity = player.rb.linearVelocity;

        velocity.x = Mathf.Lerp(
            velocity.x, player.hInput * player.maxSpeed,
            player.airFriction * Time.fixedDeltaTime
        );

        velocity.y -= player.downGravity * Time.fixedDeltaTime;

        player.rb.linearVelocity = velocity;

        if (player.isGrounded())
        {
            return states["idle"];
        }

        return null;
    }
    
}
