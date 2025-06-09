using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
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
    
    public InputManager InputManager { get; private set; }
    
    [SerializeField] public PlayerData playerData;
    [SerializeField] private Material blinkMaterial;
    private Material runtimeMaterial;
    private int blinkStrengthID;
    public int CurrentHealth { get; private set; }
    #endregion

    #region Other

    private Vector2 _workspace;

    #endregion
    
    #region Unity Functions
    
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        
        StateMachine = new PlayerStateMachine();
        IdleState = new P_IdleState(this, StateMachine, playerData, "Grounded");
        MoveState = new P_MoveState(this, StateMachine, playerData, "Grounded");
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
        runtimeMaterial = new Material(blinkMaterial);
        GetComponent<SpriteRenderer>().material = runtimeMaterial;
        blinkStrengthID = Shader.PropertyToID("_BlinkStrength");
        InputManager = GetComponent<InputManager>();
        CurrentHealth = playerData.maxHealth;
        playerData.facingDirection = 1;
        StateMachine.Initialize(IdleState);
        InputManager.EnableInput();
    }

    private void OnEnable()
    {
        
        DOTween.Play(gameObject);
    }

    private void OnDisable()
    {
        DOTween.Pause(gameObject);  
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
        
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
        Anim.SetFloat("xVelocity", Mathf.Abs(Rb.velocity.x));
        Anim.SetFloat("yVelocity", Rb.velocity.y);
    }

    #endregion

    #region Other Funtions
    
    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, playerData.maxHealth);
        Debug.Log("Player Health: " + CurrentHealth);
        StartCoroutine(DamageAnimation());
        Anim.SetTrigger("Hit");
        if(CurrentHealth == 0)
            Dead();
    }

    public void Dead()
    {
        Debug.Log("Player Dead");
        DisablePlayer();
        DOVirtual.DelayedCall(playerData.destroyAfterSeconds, (() =>
        {
            Destroy(gameObject);
            Debug.Log("Player Dead");
            GameManager.Instance.PlayerDied();
        }));
    }

    public void DisablePlayer()
    {
        InputManager.DisableInput();
        Coll.enabled = false;
    }
    
    
    IEnumerator DamageAnimation()
    {
        DOTween.To(
                () => runtimeMaterial.GetFloat(blinkStrengthID),
                x => runtimeMaterial.SetFloat(blinkStrengthID, x),
                1f,
                0.1f
            )
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(() => runtimeMaterial.SetFloat(blinkStrengthID, 0f));

        yield return new WaitForSeconds(0.25f);
    }
    #endregion
    
    
    
}
