using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public StateManager stateManager;

    [Header("Input")]
    public float hInput;
    public bool jumpInput;

    [Header("Movement")]
    public float maxSpeed = 5f;
    public float jumpForce = 10f;
    public float airFriction = 10f;
    public float groundFriction = 20f;
    public float upGravity = 10f;
    public float downGravity = 20f;

    [Header("GroundCheck")]
    public LayerMask groundLayer;
    public Transform groundCheckPosition;
    public Vector2 groundCheckSize = new Vector2(0.1f, 0.05f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stateManager = GetComponent<StateManager>();
        stateManager.Init(this);
    }

    void FixedUpdate()
    {
        stateManager.PhysicsUpdate();
    }

    public void Move(InputAction.CallbackContext context)
    {
        hInput = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        jumpInput = context.performed;
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
