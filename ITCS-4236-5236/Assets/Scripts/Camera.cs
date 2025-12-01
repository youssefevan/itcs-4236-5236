using UnityEngine;
public class FightCamera : MonoBehaviour
{
    public Transform player;
    public Transform enemy;

    public float smooth = 0.1f;

    public float minX = -5.3f;
    public float maxX = 5.3f;

    private Vector3 vel;

    void LateUpdate()
    {
        float midpoint = (player.position.x + enemy.position.x) * 0.5f;

        midpoint = Mathf.Clamp(midpoint, minX, maxX);

        Vector3 target = new Vector3(midpoint, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref vel, smooth);
    }
}
