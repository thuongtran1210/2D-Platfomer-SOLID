using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages all player components and their initialization.
/// Follows Single Responsibility Principle by handling only component management.
/// </summary>
public class PlayerComponentManager : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private bool validateComponentsOnAwake = true;
    
    private Dictionary<System.Type, Component> _components;
    private bool _isInitialized = false;

    public bool IsInitialized => _isInitialized;

    private void Awake()
    {
        _components = new Dictionary<System.Type, Component>();
        InitializeComponents();
        
        if (validateComponentsOnAwake)
        {
            ValidateRequiredComponents();
        }
    }

    private void InitializeComponents()
    {
        // Get all required components
        _components[typeof(IPlayerInput)] = GetComponent<IPlayerInput>();
        _components[typeof(IMovement)] = GetComponent<IMovement>();
        _components[typeof(IDamageable)] = GetComponent<IDamageable>();
        _components[typeof(IJump)] = GetComponent<IJump>();
        _components[typeof(IHealable)] = GetComponent<IHealable>();
        _components[typeof(IAttackable)] = GetComponent<IAttackable>();
        _components[typeof(PlayerSkillManager)] = GetComponent<PlayerSkillManager>();
        
        _isInitialized = true;
    }

    private void ValidateRequiredComponents()
    {
        var requiredTypes = new System.Type[]
        {
            typeof(IPlayerInput),
            typeof(IMovement),
            typeof(IDamageable),
            typeof(IJump),
            typeof(IHealable),
            typeof(IAttackable)
        };

        foreach (var type in requiredTypes)
        {
            if (!_components.ContainsKey(type) || _components[type] == null)
            {
                Debug.LogError($"Missing required component: {type.Name}");
                enabled = false;
                return;
            }
        }
    }

    public T GetComponent<T>() where T : class
    {
        if (_components.TryGetValue(typeof(T), out Component component))
        {
            return component as T;
        }
        
        Debug.LogWarning($"Component of type {typeof(T).Name} not found");
        return null;
    }

    public bool HasComponent<T>() where T : class
    {
        return _components.ContainsKey(typeof(T)) && _components[typeof(T)] != null;
    }
} 