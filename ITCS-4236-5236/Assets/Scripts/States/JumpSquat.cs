using UnityEngine;

public class JumpSquat : State
{
    float currentTime = 0;
    float targetTime = 0.05f; // 3 frames at 60 fps

    public override void Enter()
    {
        Debug.Log("jumpsquat");
        fighter.animator.Play("JumpSquat");
        currentTime = 0;
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        currentTime += Time.fixedDeltaTime;

        Vector2 velocity = fighter.rb.linearVelocity;

        velocity.x = Mathf.Lerp(
            velocity.x, 0f,
            (fighter.groundFriction / 3) * Time.fixedDeltaTime
        );
        velocity.y = -1f;

        fighter.rb.linearVelocity = velocity;

        if (!fighter.IsGrounded())
        {
            return states["fall"];
        }

        if (currentTime >= targetTime)
        {
            return states["jump"];
        }

        return null;
    }
}
