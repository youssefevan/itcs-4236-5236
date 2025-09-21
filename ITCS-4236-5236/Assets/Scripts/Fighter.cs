using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [Header("Components")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public StateManager stateManager;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform fighter_transform;
    public Hitbox hitbox;
    public Hurtbox hurtbox;
    public Fighter opponent;

    [Header("---Input---")]
    public IFighterInput inputType { get; set; }
    public bool aiControlled;
    [HideInInspector] public bool attackCompleted = false;
    [HideInInspector] public bool inHitStop = false;
    [HideInInspector] public float incomingStun = 0f;
    [HideInInspector] public Vector2 incomingKBAngle = Vector2.zero;
    [HideInInspector] public int incomingKBPower = 1;

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


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fighter_transform = GetComponent<Transform>();

        stateManager = GetComponent<StateManager>();
        stateManager.Init(this);
    }

    void Start()
    {
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

        if (opponent)
        {
            if (opponent.transform.position.x - transform.position.x > 0)
            {
                fighter_transform.localScale = new Vector2(4, 4);
                facing = 1;
            }
            else
            {
                fighter_transform.localScale = new Vector2(-4, 4);
                facing = -1;
            }
        }

        if (aiControlled)
        {
            ((AIInputController)inputType).SetInputs(0, 0, false, false, true, false, false);
        }
    }

    public void RecieveHit(Hitbox hb)
    {
        incomingStun = hb.damage * 0.02f;
        incomingKBAngle = hb.knockback_angle * new Vector2(-facing, 1);
        incomingKBPower = hb.knockback_power;

        if (stateManager.GetCurrentState() != stateManager.states["block"])
        {
            stateManager.ChangeState(stateManager.states["hitstun"]);
        }
        else
        {
            ((Block)stateManager.GetCurrentState()).BlockHit();
        }
    }

    public void PerformHit()
    {
        StartCoroutine(ApplyHitStop(hitbox.knockback_power / 120f));
    }

    public IEnumerator ApplyHitStop(float time)
    {
        inHitStop = true;
        animator.speed = 0;
        Vector2 cacheVel = rb.linearVelocity;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSecondsRealtime(time);
        animator.speed = 1;
        rb.linearVelocity = cacheVel;
        inHitStop = false;
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
