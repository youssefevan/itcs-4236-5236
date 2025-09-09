using UnityEngine;

public class Attack : State
{
    public int damage;
    public int knockback_power;
    public Vector2 knockback_angle;

    public Vector2 velocity_modifier;
    public int velocity_type; // 0: none, 1: one-shot, 2: additive, 3: set (linear)

    public bool modifyingVelocity;

    public void SetAttackData(int dmg, int kb_power, Vector2 kb_angle, Vector2 vel_mod, int vel_type)
    {
        damage = dmg;
        knockback_power = kb_power;
        knockback_angle = kb_angle;
        velocity_modifier = vel_mod;
        velocity_type = vel_type;
    }

    public override void Enter()
    {
        base.Enter();
        modifyingVelocity = false;
        fighter.hitbox.damage = damage;
        fighter.hitbox.knockback_power = knockback_power;
        fighter.hitbox.knockback_angle = knockback_angle.normalized;
    }

    public virtual void ApplyVelocity() { }
}
