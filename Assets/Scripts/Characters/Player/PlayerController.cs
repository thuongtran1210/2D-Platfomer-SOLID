using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement),typeof(PlayerHealth))]


public class PlayerController : MonoBehaviour, IEntity
{
    [Header("Component references")]
    private IPlayerInput input;
    private IMovement movement;
    private IDamageable damageable;
    private IJump jump;
    private IHealable healable;
    private StateMachine stateMachine;
    private Transform _transform;
    private IAttackable attackable;

    private PlayerSkillManager skillManager;

    // State instance
    private PlayerIdleState idleState;
    private PlayerRunState runState;
    private PlayerJumpState jumpState;
    private PlayerAttackState attackState;

    // State skill intance
    private PlayerSkillState skillState0;
    private PlayerSkillState skillState1;
    private PlayerSkillState skillState2;

    // Property
    public IPlayerInput Input => input;
    public IMovement Movement => movement;
    public IJump Jump => jump;
    public IDamageable Damageable => damageable;
    public IHealable Healable => healable;
    public IAttackable Attackable => attackable;

    public PlayerSkillManager SkillManager => skillManager;
    public StateMachine StateMachine => stateMachine;
    public Transform EntityTransform => _transform;

    //  Property State
    public PlayerIdleState IdleState => idleState;
    public PlayerRunState RunState => runState;
    public PlayerJumpState JumpState => jumpState;
    public PlayerAttackState AttackState => attackState;
    public PlayerSkillState SkillState0 => skillState0;
    public PlayerSkillState SkillState1 => skillState1;
    public PlayerSkillState SkillState2 => skillState2;



    // Start is called before the first frame update
    void Awake()
    {
        input = GetComponent<IPlayerInput>();
        movement = GetComponent<IMovement>();
        damageable = GetComponent<IDamageable>();
        jump = GetComponent<IJump>();
        healable = GetComponent<IHealable>();
        attackable = GetComponent<IAttackable>();

        skillManager = GetComponent<PlayerSkillManager>();

        _transform = transform;

        // 
        stateMachine = new StateMachine(this);

        idleState = new PlayerIdleState();
        runState = new PlayerRunState();
        jumpState = new PlayerJumpState();
        attackState = new PlayerAttackState();

        skillState0 = new PlayerSkillState(0);
        skillState1 = new PlayerSkillState(1);
        skillState2 = new PlayerSkillState(2);


        if (input == null || movement == null || damageable == null || healable == null || attackable == null)
        {
            Debug.LogError("Missing component requiment PlayerController!");
            enabled = false;
            return;
        }
        stateMachine.ChangeState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        Debug.Log($"Current state {stateMachine.CurrentState.GetType().Name}");


    }
     void FixedUpdate()
    {
        stateMachine.FixedUpdate();
        (input as PlayerInput)?.ResetPressState();
    }
}
