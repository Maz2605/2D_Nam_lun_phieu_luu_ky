using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }

    public Core Core { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public BoxCollider2D Coll { get; private set; }

    [SerializeField] private Data_Entity entityData;

    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float curStunResistant;
    [SerializeField] private float lastDamageTime;

    protected float CurHp;
    protected bool IsStunned;
    protected bool IsDead;

    public int LastDamageDirection { get; private set; }
    public AnimationToStateMachine AnimationToStateMachine { get; private set; }

    private Vector2 velocityWorkspace;
    
    private Collision _collision;

    private Collision Collision
    {
        get => _collision ? _collision : Core.GetCoreComponent(ref _collision);
    }
    
    private Movement _movement;

    private Movement Movement
    {
        get => _movement ? _movement : Core.GetCoreComponent(ref _movement);
    }

    protected virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();
        CurHp = entityData.health;
        StateMachine = new StateMachine();
        Anim = GetComponent<Animator>();
        Coll = GetComponent<BoxCollider2D>();
        Rb = GetComponent<Rigidbody2D>();
        AnimationToStateMachine = GetComponent<AnimationToStateMachine>();

    }
    

    protected virtual void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    
    

    public bool CheckPlayerInMinAgroRange() => Physics2D.Raycast(playerCheck.position, transform.right,
        entityData.maxAgroDistance, entityData.playerLayer);

    public bool CheckPlayerInMaxAgroRange() => Physics2D.Raycast(playerCheck.position, transform.right,
        entityData.maxAgroDistance, entityData.playerLayer);

    public bool CheckPlayerInCloseRangeActionDistance() => Physics2D.Raycast(playerCheck.position, transform.right,
        entityData.closeRangeActionDistance, entityData.playerLayer);

    public void ResetStunResistance()
    {
        IsStunned = false;
        curStunResistant = entityData.stunResistant;
    }
    
    public virtual void OnDrawGizmos()
    {
        if (Core != null)
        {
            var ledgeCheckPosition = ledgeCheck.position;
            Gizmos.DrawLine(ledgeCheckPosition, ledgeCheckPosition + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
            var playerCheckPosition = playerCheck.position;
            Gizmos.DrawWireSphere(playerCheckPosition + (Vector3)Vector2.right * entityData.closeRangeActionDistance, 0.2f);
            Gizmos.DrawWireSphere(playerCheckPosition + (Vector3)Vector2.right * entityData.minAgroDistance, 0.2f);
            Gizmos.DrawWireSphere(playerCheckPosition + (Vector3)Vector2.right * entityData.maxAgroDistance, 0.2f);
        }
    }
}