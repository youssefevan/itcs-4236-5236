using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [Header("Components")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public StateManager stateManager;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform fighterTransform;
    [HideInInspector] public SpriteRenderer sprite;
    [HideInInspector] public CapsuleCollider2D collider;
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
    [HideInInspector] public float incomingKBPower = 1f;

    [Header("Movement")]
    [SerializeField] public float maxSpeed = 8f;
    [SerializeField] public float jumpsquatTime = 0.05f;
    [SerializeField] public float landTime = 0.05f;
    [HideInInspector] public float jumpForce = 16f;
    [HideInInspector] public float airFriction = 10f;
    [HideInInspector] public float groundFriction = 20f;
    [HideInInspector] public float upGravity = 30f;
    [HideInInspector] public float downGravity = 60f;
    public bool debugStates = false;

    [Header("---Ground Check---")]
    public LayerMask groundLayer;
    public Transform groundCheckPosition;
    public Vector2 groundCheckSize = new Vector2(0.1f, 0.05f);


    [Header("Context")]
    [HideInInspector] public float facing = 1f;
    [HideInInspector] public float opponentDistance;
    [HideInInspector] public State opponentState;
    [HideInInspector] public float opponentVelocity;
    [HideInInspector] public bool opponentApproaching;
    [HideInInspector] public float opponentAttackRemaining;
    public List<AIAction> aiActions;
    private int aiFrame = 0;
    public Queue<AIAction> previousActions = new Queue<AIAction>();
    public int maxPrevActions = 4;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fighterTransform = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();

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

        if (debugStates == true)
        {
            Debug.Log(stateManager.GetCurrentState());
        }

        if (aiControlled)
        {
            aiFrame += 1;

            if (aiFrame % 10 == 0)
            {
                AIInputController controller = (AIInputController)inputType;
                CalculateDecision(controller);
            }

            if (aiFrame > 1000)
            {
                aiFrame = 0;
            }
        }
    }

    private void CalculateContext()
    {

        opponentDistance = Mathf.Abs(fighterTransform.position.x - opponent.transform.position.x);
        opponentState = opponent.stateManager.GetCurrentState();
        opponentVelocity = opponent.rb.linearVelocityX;

        if (opponentState is Attack)
        {
            AnimatorStateInfo oppAnimInfo = opponent.animator.GetCurrentAnimatorStateInfo(0);
            opponentAttackRemaining = 1f - (oppAnimInfo.normalizedTime % 1f);
        }
        else
        {
            opponentAttackRemaining = -1f;
        }


        if (facing == 1)
        {
            if (opponentVelocity < 0)
            {
                opponentApproaching = true;
            }
            else
            {
                opponentApproaching = false;
            }
        }
        else
        {
            if (opponentVelocity > 0)
            {
                opponentApproaching = true;
            }
            else
            {
                opponentApproaching = false;
            }
        }
    }

    public void AddPreviousAction(AIAction newAction)
    {
        if (previousActions.Count >= maxPrevActions)
        {
            previousActions.Dequeue();
        }

        previousActions.Enqueue(newAction);
    }

    private void CalculateDecision(AIInputController controller)
    {
        CalculateContext();

        AIAction choice = null;
        float highScore = 0f;
        foreach (AIAction a in aiActions)
        {
            float score = a.CalculateScore(this, controller);

            if (score > highScore)
            {
                highScore = score;
                choice = a;
            }
        }

        if (highScore > 0f)
        {
            choice.Execute(this, controller);
        }
    }

    public void FaceOpponent()
    {
        if (opponent)
        {
            if (opponent.transform.position.x - transform.position.x > 0)
            {
                fighterTransform.localScale = new Vector2(6, 6);
                facing = 1;
            }
            else
            {
                fighterTransform.localScale = new Vector2(-6, 6);
                facing = -1;
            }
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
        hitbox.enabled = false;
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
