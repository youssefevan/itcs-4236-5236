using UnityEngine;

public class TitleAnimate : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }
}
