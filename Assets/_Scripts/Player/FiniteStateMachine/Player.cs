using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region States

    public PlayerStateMachine StateMachine { get; private set; }
    public P_GroundedState GroundedState { get; private set; }
    public P_MoveState MoveState { get; private set; }
    public P_IdleState IdleState { get; private set; }
    public P_AbilityStates AbilityState { get; private set; }
    public P_JumpState JumpState { get; private set; }
    public P_InAirState InAirState { get; private set; }
    #endregion

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public BoxCollider2D Coll { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    
    [SerializeField] public PlayerData playerData;
    #endregion

    #region Other

    private Vector2 _workspace;

    #endregion
    
    #region Unity Functions
    
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        
        StateMachine = new PlayerStateMachine();
        IdleState = new P_IdleState(this, StateMachine, playerData, "Idle");
        MoveState = new P_MoveState(this, StateMachine, playerData, "Move");
        GroundedState = new P_GroundedState(this, StateMachine, playerData, "Grounded");
        InAirState = new P_InAirState(this, StateMachine, playerData, "InAir");
        AbilityState = new P_AbilityStates(this, StateMachine, playerData, "Ability");
        JumpState = new P_JumpState(this, StateMachine, playerData, "InAir");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        Coll = GetComponent<BoxCollider2D>();
        Rb = GetComponent<Rigidbody2D>();
        playerData.facingDirection = 1;
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Other Funtions

    private void AninmationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    private void AnimationFinishedTrigger()
    {
        StateMachine.CurrentState.AnimationFinishedTrigger();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var Item = other.gameObject.GetComponent<Base_Item>();
        Item.Effect(this);
    }

    #endregion
    
}
