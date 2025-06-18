using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Refactored PlayerSkillManager with Observer pattern and better error handling.
/// Follows SOLID principles and design patterns.
/// </summary>
public class PlayerSkillManagerRefactored : MonoBehaviour
{
    [Header("Skill Configuration")]
    [SerializeField] private List<SkillData> skillDataList = new List<SkillData>();
    [SerializeField] private bool enableSkillLogging = false;
    
    private Dictionary<int, ISkill> _activeSkills;
    private IEntity _playerEntity;
    
    // Observer pattern events
    public event Action<int, ISkill> OnSkillInitialized;
    public event Action<int, ISkill> OnSkillUsed;
    public event Action<int> OnSkillCooldownStarted;
    public event Action<int> OnSkillCooldownFinished;
    public event Action<string> OnSkillError;

    private void Awake()
    {
        _activeSkills = new Dictionary<int, ISkill>();
        _playerEntity = GetComponent<IEntity>();

        ValidateEntity();
        InitializeSkills();
    }

    private void ValidateEntity()
    {
        if (_playerEntity == null)
        {
            LogError("PlayerSkillManager requires an IEntity component on the same GameObject!");
            enabled = false;
            return;
        }
    }

    private void InitializeSkills()
    {
        for (int i = 0; i < skillDataList.Count; i++)
        {
            var skillData = skillDataList[i];
            if (skillData != null)
            {
                InitializeSkill(i, skillData);
            }
            else
            {
                LogWarning($"Skill data at index {i} is null. Skipping initialization.");
            }
        }
    }

    private void InitializeSkill(int index, SkillData data)
    {
        try
        {
            var skill = SkillFactory.CreateSkill(data);
            if (skill != null)
            {
                skill.SkillData = data;
                _activeSkills.Add(index, skill);
                
                LogSkill($"Skill '{data.skillName}' (ID: {data.skillId}) initialized for index {index}");
                OnSkillInitialized?.Invoke(index, skill);
            }
            else
            {
                LogError($"Failed to create skill for data: {data.skillName}");
            }
        }
        catch (Exception ex)
        {
            LogError($"Exception while initializing skill {index}: {ex.Message}");
        }
    }

    /// <summary>
    /// Checks if a skill is ready to use.
    /// </summary>
    /// <param name="skillIndex">Index of the skill to check.</param>
    /// <returns>True if skill is ready, false otherwise.</returns>
    public bool IsSkillReady(int skillIndex)
    {
        if (!_activeSkills.TryGetValue(skillIndex, out ISkill skill))
        {
            LogWarning($"Skill with index {skillIndex} not found.");
            return false;
        }

        return skill.CanUseSkill();
    }

    /// <summary>
    /// Performs a skill if it's ready.
    /// </summary>
    /// <param name="skillIndex">Index of the skill to perform.</param>
    /// <returns>True if skill was performed successfully, false otherwise.</returns>
    public bool PerformSkill(int skillIndex)
    {
        if (!_activeSkills.TryGetValue(skillIndex, out ISkill skill))
        {
            LogWarning($"Skill with index {skillIndex} not found.");
            return false;
        }

        if (!skill.CanUseSkill())
        {
            LogWarning($"Skill {skillIndex} is not ready or cannot be used.");
            return false;
        }

        try
        {
            skill.PerformSkill(_playerEntity);
            LogSkill($"Skill {skillIndex} performed successfully");
            OnSkillUsed?.Invoke(skillIndex, skill);
            return true;
        }
        catch (Exception ex)
        {
            LogError($"Exception while performing skill {skillIndex}: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Gets a skill by index.
    /// </summary>
    /// <param name="skillIndex">Index of the skill.</param>
    /// <returns>The skill if found, null otherwise.</returns>
    public ISkill GetSkill(int skillIndex)
    {
        return _activeSkills.TryGetValue(skillIndex, out ISkill skill) ? skill : null;
    }

    /// <summary>
    /// Gets all active skills.
    /// </summary>
    /// <returns>Dictionary of all active skills.</returns>
    public Dictionary<int, ISkill> GetAllSkills()
    {
        return new Dictionary<int, ISkill>(_activeSkills);
    }

    /// <summary>
    /// Adds a custom skill to the manager.
    /// </summary>
    /// <param name="skillIndex">Index for the skill.</param>
    /// <param name="skill">The skill to add.</param>
    /// <returns>True if added successfully, false if index already exists.</returns>
    public bool AddCustomSkill(int skillIndex, ISkill skill)
    {
        if (_activeSkills.ContainsKey(skillIndex))
        {
            LogWarning($"Skill index {skillIndex} already exists. Cannot add custom skill.");
            return false;
        }

        _activeSkills.Add(skillIndex, skill);
        LogSkill($"Custom skill added at index {skillIndex}");
        OnSkillInitialized?.Invoke(skillIndex, skill);
        return true;
    }

    /// <summary>
    /// Removes a skill from the manager.
    /// </summary>
    /// <param name="skillIndex">Index of the skill to remove.</param>
    /// <returns>True if removed successfully, false if not found.</returns>
    public bool RemoveSkill(int skillIndex)
    {
        if (_activeSkills.Remove(skillIndex))
        {
            LogSkill($"Skill at index {skillIndex} removed");
            return true;
        }
        
        LogWarning($"Skill at index {skillIndex} not found for removal.");
        return false;
    }

    private void Update()
    {
        foreach (var skill in _activeSkills.Values)
        {
            try
            {
                skill.UpdateSkill();
            }
            catch (Exception ex)
            {
                LogError($"Exception in skill update: {ex.Message}");
            }
        }
    }

    #region Logging Methods
    private void LogSkill(string message)
    {
        if (enableSkillLogging)
        {
            Debug.Log($"[SkillManager] {message}");
        }
    }

    private void LogWarning(string message)
    {
        Debug.LogWarning($"[SkillManager] {message}");
        OnSkillError?.Invoke(message);
    }

    private void LogError(string message)
    {
        Debug.LogError($"[SkillManager] {message}");
        OnSkillError?.Invoke(message);
    }
    #endregion
} 