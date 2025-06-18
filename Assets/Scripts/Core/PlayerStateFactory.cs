using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Factory for creating player states.
/// Follows Factory Pattern and Open/Closed Principle.
/// </summary>
public class PlayerStateFactory
{
    private readonly AnimationManager _animationManager;
    private readonly PlayerController _playerController;
    private readonly Dictionary<System.Type, IState> _stateCache;

    public PlayerStateFactory(AnimationManager animationManager, PlayerController playerController)
    {
        _animationManager = animationManager;
        _playerController = playerController;
        _stateCache = new Dictionary<System.Type, IState>();
    }

    public IState CreateState<T>() where T : IState
    {
        var stateType = typeof(T);
        
        // Check cache first
        if (_stateCache.TryGetValue(stateType, out IState cachedState))
        {
            return cachedState;
        }

        // Create new state based on type
        IState newState = stateType.Name switch
        {
            nameof(PlayerIdleState) => new PlayerIdleState(_animationManager),
            nameof(PlayerRunState) => new PlayerRunState(_animationManager),
            nameof(PlayerJumpState) => new PlayerJumpState(_animationManager, _playerController),
            nameof(PlayerAttackState) => new PlayerAttackState(_animationManager),
            nameof(PlayerSkillState) => CreateSkillState(0), // Default skill state
            _ => null
        };

        if (newState != null)
        {
            _stateCache[stateType] = newState;
        }

        return newState;
    }

    public PlayerSkillState CreateSkillState(int skillIndex)
    {
        return new PlayerSkillState(skillIndex);
    }

    public void PreloadStates()
    {
        // Preload commonly used states
        CreateState<PlayerIdleState>();
        CreateState<PlayerRunState>();
        CreateState<PlayerJumpState>();
        CreateState<PlayerAttackState>();
    }

    public void ClearCache()
    {
        _stateCache.Clear();
    }
} 