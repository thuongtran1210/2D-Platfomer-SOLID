using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAnimationStrategy : IAnimationStrategy
{
    protected readonly string _animationName;
    protected readonly float _duration;
    protected bool _isPlaying;
    protected BaseAnimationStrategy(string animationName, float duration)
    {
        _animationName = animationName;
        _duration = duration;
    }

    public virtual void Play(Animator animator)
    {
        _isPlaying = true;
    }

    public virtual void Stop(Animator animator)
    {
        _isPlaying = false;
    }

    // Property
    public bool IsPlaying => _isPlaying;
    public float Duration => _duration;

}
