using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages state transitions based on player input and conditions.
/// Follows Single Responsibility Principle by handling only transition logic.
/// </summary>
public class PlayerStateTransitionManager
{
    private readonly PlayerStateRegistry _stateRegistry;
    private readonly PlayerController _playerController;
    private readonly Dictionary<string, System.Func<bool>> _transitionConditions;

    public PlayerStateTransitionManager(PlayerStateRegistry stateRegistry, PlayerController playerController)
    {
        _stateRegistry = stateRegistry;
        _playerController = playerController;
        _transitionConditions = new Dictionary<string, System.Func<bool>>();
        
        InitializeTransitionConditions();
    }

    private void InitializeTransitionConditions()
    {
        // Define transition conditions
        _transitionConditions["ToIdle"] = () => 
            !_playerController.Input.HorizontalInput.Equals(0f) && 
            _playerController.Jump.IsGrounded;

        _transitionConditions["ToRun"] = () => 
            !_playerController.Input.HorizontalInput.Equals(0f) && 
            _playerController.Jump.IsGrounded;

        _transitionConditions["ToJump"] = () => 
            _playerController.Input.JumpPressed && 
            _playerController.Jump.IsGrounded;

        _transitionConditions["ToAttack"] = () => 
            _playerController.Input.AttackPressed && 
            _playerController.Jump.IsGrounded;

        _transitionConditions["ToSkill0"] = () => 
            _playerController.Input.Skill1Pressed && 
            _playerController.SkillManager.IsSkillReady(0);

        _transitionConditions["ToSkill1"] = () => 
            _playerController.Input.Skill2Pressed && 
            _playerController.SkillManager.IsSkillReady(1);

        _transitionConditions["ToSkill2"] = () => 
            _playerController.Input.Skill3Pressed && 
            _playerController.SkillManager.IsSkillReady(2);
    }

    public IState GetNextState(IState currentState)
    {
        string currentStateName = GetStateName(currentState);
        
        // Check all possible transitions
        foreach (var condition in _transitionConditions)
        {
            if (condition.Value() && IsValidTransition(currentStateName, condition.Key))
            {
                return GetTargetState(condition.Key);
            }
        }

        return currentState; // No transition needed
    }

    private string GetStateName(IState state)
    {
        return state.GetType().Name.Replace("Player", "").Replace("State", "");
    }

    private bool IsValidTransition(string currentState, string transitionKey)
    {
        string targetStateName = GetTargetStateName(transitionKey);
        return _stateRegistry.CanTransitionTo(currentState, targetStateName);
    }

    private IState GetTargetState(string transitionKey)
    {
        string targetStateName = GetTargetStateName(transitionKey);
        return _stateRegistry.GetState(targetStateName);
    }

    private string GetTargetStateName(string transitionKey)
    {
        return transitionKey switch
        {
            "ToIdle" => "Idle",
            "ToRun" => "Run",
            "ToJump" => "Jump",
            "ToAttack" => "Attack",
            "ToSkill0" => "Skill0",
            "ToSkill1" => "Skill1",
            "ToSkill2" => "Skill2",
            _ => "Idle"
        };
    }

    public void AddCustomTransitionCondition(string conditionName, System.Func<bool> condition)
    {
        if (!_transitionConditions.ContainsKey(conditionName))
        {
            _transitionConditions[conditionName] = condition;
        }
    }

    public bool HasTransitionCondition(string conditionName)
    {
        return _transitionConditions.ContainsKey(conditionName);
    }
} 