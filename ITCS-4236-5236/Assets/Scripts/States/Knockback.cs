using UnityEngine;

public class Knockback : State
{
    float currentTime = 0;
    float stunTime;

    public override void Enter()
    {
        base.Enter();
        currentTime = 0;
        stunTime = fighter.incomingKBPower / 60f;

        fighter.rb.linearVelocity = fighter.incomingKBAngle * fighter.incomingKBPower;
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();
        currentTime += Time.fixedDeltaTime;

        Vector2 velocity = fighter.rb.linearVelocity;

        velocity.x = Mathf.Lerp(
            velocity.x, fighter.inputType.moveInput * fighter.maxSpeed, 1 * Time.fixedDeltaTime
        );

        if (velocity.y < 0)
        {
            velocity.y -= fighter.upGravity * Time.fixedDeltaTime;
        }
        else
        {
            velocity.y -= fighter.downGravity * Time.fixedDeltaTime;
        }


        fighter.rb.linearVelocity = velocity;

        if (currentTime >= stunTime)
        {
            return states["idle"];
        }

        return null;
    }
}
