using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Registry for managing player states and their transitions.
/// Follows Registry Pattern for better state management.
/// </summary>
public class PlayerStateRegistry
{
    private readonly Dictionary<string, IState> _states;
    private readonly Dictionary<string, List<string>> _allowedTransitions;
    private readonly PlayerStateFactory _stateFactory;

    public PlayerStateRegistry(PlayerStateFactory stateFactory)
    {
        _states = new Dictionary<string, IState>();
        _allowedTransitions = new Dictionary<string, List<string>>();
        _stateFactory = stateFactory;
        
        InitializeStates();
        InitializeTransitions();
    }

    private void InitializeStates()
    {
        // Register all player states
        RegisterState("Idle", _stateFactory.CreateState<PlayerIdleState>());
        RegisterState("Run", _stateFactory.CreateState<PlayerRunState>());
        RegisterState("Jump", _stateFactory.CreateState<PlayerJumpState>());
        RegisterState("Attack", _stateFactory.CreateState<PlayerAttackState>());
        
        // Register skill states
        for (int i = 0; i < 3; i++)
        {
            RegisterState($"Skill{i}", _stateFactory.CreateSkillState(i));
        }
    }

    private void InitializeTransitions()
    {
        // Define allowed state transitions
        AddTransition("Idle", new[] { "Run", "Jump", "Attack", "Skill0", "Skill1", "Skill2" });
        AddTransition("Run", new[] { "Idle", "Jump", "Attack", "Skill0", "Skill1", "Skill2" });
        AddTransition("Jump", new[] { "Idle", "Run" });
        AddTransition("Attack", new[] { "Idle", "Run" });
        AddTransition("Skill0", new[] { "Idle", "Run" });
        AddTransition("Skill1", new[] { "Idle", "Run" });
        AddTransition("Skill2", new[] { "Idle", "Run" });
    }

    public void RegisterState(string stateName, IState state)
    {
        if (!_states.ContainsKey(stateName))
        {
            _states[stateName] = state;
        }
    }

    public IState GetState(string stateName)
    {
        return _states.TryGetValue(stateName, out IState state) ? state : null;
    }

    public bool CanTransitionTo(string fromState, string toState)
    {
        if (!_allowedTransitions.ContainsKey(fromState))
            return false;

        return _allowedTransitions[fromState].Contains(toState);
    }

    private void AddTransition(string fromState, string[] toStates)
    {
        if (!_allowedTransitions.ContainsKey(fromState))
        {
            _allowedTransitions[fromState] = new List<string>();
        }

        foreach (var toState in toStates)
        {
            if (!_allowedTransitions[fromState].Contains(toState))
            {
                _allowedTransitions[fromState].Add(toState);
            }
        }
    }

    public List<string> GetAllowedTransitions(string fromState)
    {
        return _allowedTransitions.TryGetValue(fromState, out var transitions) 
            ? new List<string>(transitions) 
            : new List<string>();
    }

    public bool StateExists(string stateName)
    {
        return _states.ContainsKey(stateName);
    }
} 