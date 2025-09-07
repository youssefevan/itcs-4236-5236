using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fighter : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public StateManager stateManager;
    public Animator animator;

    [Header("Input")]
    public float moveInput;
    public bool jumpInput;
    public bool blockInput;
    public bool crouchInput;
    public Vector2 aimInput;
    public bool punchInput;
    public bool kickInput;

    [Header("Movement")]
    public float maxSpeed = 8f;
    public float jumpForce = 16f;
    public float airFriction = 10f;
    public float groundFriction = 20f;
    public float upGravity = 30f;
    public float downGravity = 60f;

    [Header("GroundCheck")]
    public LayerMask groundLayer;
    public Transform groundCheckPosition;
    public Vector2 groundCheckSize = new Vector2(0.1f, 0.05f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateManager = GetComponent<StateManager>();
        stateManager.Init(this);
    }

    void FixedUpdate()
    {
        stateManager.PhysicsUpdate();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
    }

    public void Aim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        jumpInput = context.performed;
    }

    public void Block(InputAction.CallbackContext context)
    {
        blockInput = context.performed;
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        crouchInput = context.performed;
    }

    public void Punch(InputAction.CallbackContext context)
    {
        punchInput = context.performed;
    }

    public void Kick(InputAction.CallbackContext context)
    {
        kickInput = context.performed;
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapBox(groundCheckPosition.position, groundCheckSize, 0, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPosition.position, groundCheckSize);
    }
}
