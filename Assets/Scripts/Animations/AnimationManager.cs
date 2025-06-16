using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager 
{
    private Dictionary<Type, IAnimationStrategy> _strategies;
    private readonly Animator _animator;
    private IAnimationStrategy _currentStrategy;


    //Events system;
    public event Action<string> OnAnimationStarted;
    public event Action<string> OnAnimationCompleted;


    public AnimationManager(Animator animator)
    {
        _animator = animator;
        _strategies = new Dictionary<Type, IAnimationStrategy>()
        {
            { typeof(PlayerIdleState), new IdleAnimationStrategy() },
            { typeof(PlayerRunState), new RunAnimationStrategy() },
            { typeof(PlayerJumpState), new JumpAnimationStrategy() },
            { typeof(PlayerAttackState), new AttackAnimationStrategy() }

        };
    }
    public void PlayAnimationForState(IState state)
    {
        if (_animator == null) return;
        if (state is PlayerSkillState skillState)
        {
            var strategy = new SkillAnimationStrategy(skillState.SkillIndex);
            strategy.Play(_animator);
        }
        else if (_strategies.TryGetValue(state.GetType(), out var strategy))
        {
            strategy.Play(_animator);
          
        }
        OnAnimationStarted?.Invoke(state.GetType().Name);
    }
    public void StopAnimationForState(IState state)
    {
        if (_strategies.TryGetValue(state.GetType(), out var strategy))
        {
            strategy.Stop(_animator);
        }
        OnAnimationCompleted?.Invoke(state.GetType().Name);
    }
}
