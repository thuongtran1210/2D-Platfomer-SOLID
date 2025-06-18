using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager 
{
    private readonly Dictionary<Type, IAnimationStrategy> _strategies;
    private readonly Animator _animator;
    private IAnimationStrategy _currentStrategy;
    private float _currentAnimationTime;

    public event Action<string> OnAnimationStarted;
    public event Action<string> OnAnimationCompleted;
    public event Action<float> OnAnimationProgress;


    public AnimationManager(Animator animator)
    {
        _animator = animator;
        _strategies = new Dictionary<Type, IAnimationStrategy>();
        InitStrategies();

    }
    public void RegisterStrategy(IAnimationStrategy strategy)
    {
        _strategies[strategy.GetType()] = strategy;
    }
    private void InitStrategies()
    {
        RegisterStrategy(new IdleAnimationStrategy());
        RegisterStrategy(new RunAnimationStrategy());
        RegisterStrategy(new JumpAnimationStrategy());
        RegisterStrategy(new AttackAnimationStrategy());
    }
    public void PlayAnimationForState(IState state)
    {
        if (_animator == null) return;
        if (state is PlayerSkillState skillState)
        {
            var strategy = new SkillAnimationStrategy(skillState.SkillIndex);
            PlayStrategy(strategy);
        }
        else if (_strategies.TryGetValue(state.GetType(), out var strategy))
        {
            PlayStrategy(strategy);
        }
    }
    public void StopAnimationForState(IState state)
    {
        if (_animator == null) return;

        if (state is PlayerSkillState skillState)
        {
            var strategy = new SkillAnimationStrategy(skillState.SkillIndex);
            strategy.Stop(_animator);
        }
        else if (_strategies.TryGetValue(state.GetType(), out var strategy))
        {
            strategy.Stop(_animator);
        }
    }
    private void PlayStrategy(IAnimationStrategy strategy)
    {
        _currentStrategy?.Stop(_animator);
        _currentStrategy = strategy;
        _currentStrategy.Play(_animator);
        _currentAnimationTime = 0f;
        OnAnimationStarted?.Invoke(_currentStrategy.GetType().Name);
    }
    public void Update()
    {
        if (_currentStrategy != null && _currentStrategy.IsPlaying)
        {
            _currentAnimationTime += Time.deltaTime;
            float progress = _currentAnimationTime / _currentStrategy.Duration;
            OnAnimationProgress?.Invoke(progress);

            if (_currentAnimationTime >= _currentStrategy.Duration)
            {
                _currentStrategy.Stop(_animator);
                OnAnimationCompleted?.Invoke(_currentStrategy.GetType().Name);
            }
        }
    }
    // Helper methods 
    public void UpdateJumpVelocity(float velocity)
    {
        if (_currentStrategy is JumpAnimationStrategy jumpStrategy)
        {
            jumpStrategy.UpdateJumpVelocity(_animator, velocity);
        }
    }

    public void UpdateRunSpeed(float speed)
    {
        if (_currentStrategy is RunAnimationStrategy runStrategy)
        {
            runStrategy.UpdateSpeed(_animator, speed);
        }
    }

    public void UpdateAttackCombo(int comboCount)
    {
        if (_currentStrategy is AttackAnimationStrategy attackStrategy)
        {
            attackStrategy.SetAttackCombo(_animator, comboCount);
        }
    }
}
