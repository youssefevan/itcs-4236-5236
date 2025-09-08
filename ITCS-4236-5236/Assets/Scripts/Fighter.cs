using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [Header("Components")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public StateManager stateManager;
    [HideInInspector] public Animator animator;

    [Header("---Input---")]
    public IFighterInput inputType { get; set; }
    public bool aiControlled;
    [HideInInspector] public bool attackCompleted = false;

    [Header("Movement")]
    [HideInInspector] public float maxSpeed = 8f;
    [HideInInspector] public float jumpForce = 16f;
    [HideInInspector] public float airFriction = 10f;
    [HideInInspector] public float groundFriction = 20f;
    [HideInInspector] public float upGravity = 30f;
    [HideInInspector] public float downGravity = 60f;
    [HideInInspector] public float facing = 1f;

    [Header("---Ground Check---")]
    public LayerMask groundLayer;
    public Transform groundCheckPosition;
    public Vector2 groundCheckSize = new Vector2(0.1f, 0.05f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateManager = GetComponent<StateManager>();
        stateManager.Init(this);

        if (aiControlled)
        {
            inputType = GetComponent<AIInputController>();
        }
        else
        {
            inputType = GetComponent<PlayerInputController>();
        }

    }

    void FixedUpdate()
    {
        stateManager.PhysicsUpdate();

        /*if (inputType.moveInput != 0)
        {
            if (inputType.moveInput > 0)
            {
                facing = 1;
            }
            else if (inputType.moveInput < 0)
            {
                facing = -1;
            }
        }*/
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheckPosition.position, groundCheckSize, 0, groundLayer);
    }

    public void AttackCompleted()
    {
        stateManager.ChangeState(stateManager.states["idle"]);
    }

    public void ApplyAttackVelocity()
    {
        State currentState = stateManager.GetCurrentState();

        if (currentState is Attack attack)
        {
            attack.ApplyVelocity();
        }
    }

    public void StartModifyingVelocity()
    {
        State currentState = stateManager.GetCurrentState();

        if (currentState is Attack attack)
        {
            attack.modifyingVelocity = true;
        }
    }

    public void EndModifyingVelocity()
    {
        State currentState = stateManager.GetCurrentState();

        if (currentState is Attack attack)
        {
            attack.modifyingVelocity = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPosition.position, groundCheckSize);
    }
}
