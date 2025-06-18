using UnityEngine;

/// <summary>
/// Refactored PlayerController that follows SOLID principles and design patterns.
/// - Single Responsibility: Each manager handles one aspect
/// - Open/Closed: Easy to extend with new states/skills
/// - Dependency Inversion: Depends on abstractions
/// - Interface Segregation: Uses specific interfaces
/// </summary>
[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement), typeof(PlayerHealth))]
public class PlayerControllerRefactored : MonoBehaviour, IEntity
{
    [Header("Component Management")]
    [SerializeField] private bool enableDebugLogging = false;
    
    // Core managers
    private PlayerComponentManager _componentManager;
    private PlayerStateFactory _stateFactory;
    private PlayerStateRegistry _stateRegistry;
    private PlayerStateTransitionManager _transitionManager;
    
    // Core systems
    private StateMachine _stateMachine;
    private AnimationManager _animationManager;
    
    // Cached components
    private Transform _transform;
    
    #region IEntity Implementation
    public Transform EntityTransform => _transform;
    public IMovement Movement => _componentManager.GetComponent<IMovement>();
    public IJump Jump => _componentManager.GetComponent<IJump>();
    public IDamageable Damageable => _componentManager.GetComponent<IDamageable>();
    public IHealable Healable => _componentManager.GetComponent<IHealable>();
    #endregion
    
    #region Public Properties
    public IPlayerInput Input => _componentManager.GetComponent<IPlayerInput>();
    public IAttackable Attackable => _componentManager.GetComponent<IAttackable>();
    public PlayerSkillManager SkillManager => _componentManager.GetComponent<PlayerSkillManager>();
    public StateMachine StateMachine => _stateMachine;
    public AnimationManager AnimationManager => _animationManager;
    public PlayerStateRegistry StateRegistry => _stateRegistry;
    #endregion

    private void Awake()
    {
        InitializeComponents();
        InitializeSystems();
        StartStateMachine();
    }

    private void InitializeComponents()
    {
        _componentManager = GetComponent<PlayerComponentManager>();
        if (_componentManager == null)
        {
            _componentManager = gameObject.AddComponent<PlayerComponentManager>();
        }
        
        _transform = transform;
        
        // Wait for component manager to initialize
        if (!_componentManager.IsInitialized)
        {
            Debug.LogError("Component manager not initialized!");
            enabled = false;
            return;
        }
    }

    private void InitializeSystems()
    {
        // Initialize animation system
        var animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component required!");
            enabled = false;
            return;
        }
        
        _animationManager = new AnimationManager(animator);
        
        // Initialize state management system
        _stateFactory = new PlayerStateFactory(_animationManager, this);
        _stateRegistry = new PlayerStateRegistry(_stateFactory);
        _transitionManager = new PlayerStateTransitionManager(_stateRegistry, this);
        
        // Initialize state machine
        _stateMachine = new StateMachine(this);
        
        LogDebug("All systems initialized successfully");
    }

    private void StartStateMachine()
    {
        var idleState = _stateRegistry.GetState("Idle");
        if (idleState != null)
        {
            _stateMachine.ChangeState(idleState);
            LogDebug("State machine started with Idle state");
        }
        else
        {
            Debug.LogError("Failed to get initial state!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (!enabled) return;
        
        // Check for state transitions
        var nextState = _transitionManager.GetNextState(_stateMachine.CurrentState);
        if (nextState != _stateMachine.CurrentState)
        {
            _stateMachine.ChangeState(nextState);
            LogDebug($"State changed to: {nextState.GetType().Name}");
        }
        
        // Update current state
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        if (!enabled) return;
        
        _stateMachine.FixedUpdate();
        
        // Reset input states
        if (Input is PlayerInput playerInput)
        {
            playerInput.ResetPressState();
        }
    }

    private void LogDebug(string message)
    {
        if (enableDebugLogging)
        {
            Debug.Log($"[PlayerController] {message}");
        }
    }

    #region Public Methods
    public void AddCustomState(string stateName, IState state)
    {
        _stateRegistry.RegisterState(stateName, state);
        LogDebug($"Custom state '{stateName}' registered");
    }

    public void AddCustomTransitionCondition(string conditionName, System.Func<bool> condition)
    {
        _transitionManager.AddCustomTransitionCondition(conditionName, condition);
        LogDebug($"Custom transition condition '{conditionName}' added");
    }

    public bool CanTransitionTo(string targetState)
    {
        string currentStateName = _stateMachine.CurrentState.GetType().Name.Replace("Player", "").Replace("State", "");
        return _stateRegistry.CanTransitionTo(currentStateName, targetState);
    }

    public void ForceStateChange(string stateName)
    {
        var newState = _stateRegistry.GetState(stateName);
        if (newState != null)
        {
            _stateMachine.ChangeState(newState);
            LogDebug($"Forced state change to: {stateName}");
        }
        else
        {
            Debug.LogWarning($"State '{stateName}' not found!");
        }
    }
    #endregion
} 