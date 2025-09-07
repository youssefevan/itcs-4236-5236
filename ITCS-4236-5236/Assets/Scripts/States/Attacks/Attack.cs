using UnityEngine;

public class Attack : State
{
    public int damage;
    public int knockback_power;
    public Vector2 knockback_angle;

    public Vector2 velocity_modifier;
    public int velocity_type; // 0: none, 1: one-shot, 2: additive, 3: set (linear)

    public override void Enter()
    {
        base.Enter();
        // play animation (which also sets transform of hitbox)
        // enable hitbox settings
        // set hitbox variables

        Vector2 velocity = fighter.rb.linearVelocity;

        if (velocity_type == 0)
        {
            velocity = velocity_modifier;
        }

        fighter.rb.linearVelocity = velocity;
    }
}
