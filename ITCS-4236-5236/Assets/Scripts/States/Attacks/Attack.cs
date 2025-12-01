using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Attack : State
{
    public int damage;
    public int knockback_power;
    public Vector2 knockback_angle;

    public Vector2 velocity_modifier;
    public int velocity_type; // 0: none, 1: one-shot, 2: additive, 3: set (linear)
    public bool is_low;

    public bool modifyingVelocity;

    public void SetAttackData(int dmg, int kb_power, Vector2 kb_angle, Vector2 vel_mod, int vel_type, bool low)
    {
        damage = dmg;
        knockback_power = kb_power;
        knockback_angle = kb_angle;
        velocity_modifier = vel_mod;
        velocity_type = vel_type;
        is_low = low;
    }

    public override void Enter()
    {
        base.Enter();
        modifyingVelocity = false;
        fighter.hitbox.damage = damage;
        fighter.hitbox.knockback_power = knockback_power;
        fighter.hitbox.knockback_angle = knockback_angle.normalized;
        fighter.hitbox.is_low = is_low;
    }

    public override State? PhysicsUpdate()
    {
        base.PhysicsUpdate();

        AnimatorStateInfo info = fighter.animator.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= 1f)
        {
            return states["idle"];
        }

        return null;
    }


    public virtual void ApplyVelocity() { }
}