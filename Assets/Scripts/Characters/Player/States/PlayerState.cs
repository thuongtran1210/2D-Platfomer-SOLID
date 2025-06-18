using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IState
{
    protected readonly AnimationManager _animationManager;
    protected readonly PlayerController _player;
    protected float _stateTimer;
    protected bool _isStateComplete;
    protected PlayerState(AnimationManager animationManager, PlayerController player)
    {
        _animationManager = animationManager;
        _player = player;
    }

    public virtual void Enter(IEntity entity)
    {
        _stateTimer = 0f;
        _isStateComplete = false;
        _animationManager.PlayAnimationForState(this);
    }

    public virtual void Exit(IEntity entity)
    {
        _animationManager.StopAnimationForState(this);
    }

    public virtual void FixedUpdate(IEntity entity)
    {
        if (_isStateComplete) return;
        _stateTimer += Time.fixedDeltaTime;
    }

    public abstract void Update(IEntity entity);

    protected virtual bool CanTransitionToState<T>() where T : IState
    {
        return true;
    }

    protected void ChangeState<T>() where T : IState
    {
        if (CanTransitionToState<T>())
        {
            _player.StateMachine.ChangeState(_player.IdleState);
        }
    }


}
