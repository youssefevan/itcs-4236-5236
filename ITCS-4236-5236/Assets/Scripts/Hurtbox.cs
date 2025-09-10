using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public Fighter fighter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hitbox hitbox = other.GetComponent<Hitbox>();
        if (hitbox != null)
        {
            fighter.RecieveHit(hitbox);
        }
    }
}
