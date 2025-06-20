using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState
{
    private float attackDuration = 0.2f; // animation attack
    private float attackTimer;
    private readonly AnimationManager _animationManager;
    public PlayerAttackState(AnimationManager animationManager)
    {
        _animationManager = animationManager;   
    }
    public void Enter(IEntity entity)
    {
        PlayerController player = entity as PlayerController;
        if (player == null) return;


        player.Attackable.PerformAttack(); 
        attackTimer = attackDuration;
        _animationManager.PlayAnimationForState(this);
        
    }

    public void Exit(IEntity entity)
    {
        
    }

    public void FixedUpdate(IEntity entity)
    {
        
    }

    public void Update(IEntity entity)
    {
        PlayerController player = entity as PlayerController;
        if (player == null) return;

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            player.StateMachine.ChangeState(player.IdleState);

        }
    }
}
