using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;
    private IAnimationStrategy currentAnimationStrategy;
    private Dictionary<System.Type, IAnimationStrategy> animationStrategies;

    [Header("Blend Tree Parameters")]
    [SerializeField] private float movementBlendSpeed = 1f;
    [SerializeField] private float combatBlendSpeed = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        InitializeStrategies();
    }
    private void InitializeStrategies()
    {
        animationStrategies = new Dictionary<System.Type, IAnimationStrategy>
        {
            { typeof(MovementBlendTreeStrategy), new MovementBlendTreeStrategy() },
            { typeof(CombatBlendTreeStrategy), new CombatBlendTreeStrategy() },
        };
    }
    public void SetAnimationStrategy<T>() where T : IAnimationStrategy
    {
        if (currentAnimationStrategy != null)
        {
            currentAnimationStrategy.StopAnimation(animator);
        }

        if (animationStrategies.TryGetValue(typeof(T), out IAnimationStrategy strategy))
        {
            currentAnimationStrategy = strategy;
            currentAnimationStrategy.PlayAnimation(animator);
        }
    }
    public void UpdateBlendValue(float value)
    {
        currentAnimationStrategy?.UpdateBlendTree(animator, value);
    }



}
