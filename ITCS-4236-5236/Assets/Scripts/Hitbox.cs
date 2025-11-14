using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Fighter fighter;

    [HideInInspector] public int damage;
    [HideInInspector] public int knockback_power;
    [HideInInspector] public Vector2 knockback_angle;
    [HideInInspector] public bool is_low;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hurtbox hurtbox = other.GetComponent<Hurtbox>();
        if (hurtbox != null)
        {
            fighter.PerformHit();
        }
    }

}
