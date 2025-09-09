using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Fighter fighter;

    [HideInInspector] public int damage;
    [HideInInspector] public int knockback_power;
    [HideInInspector] public Vector2 knockback_angle;

}
