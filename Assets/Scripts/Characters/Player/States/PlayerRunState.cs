
using System.Diagnostics;
using UnityEngine;

public class PlayerRunState : IState
{
    private readonly AnimationManager _animationManager;
    private float speed = 8f;
    public PlayerRunState(AnimationManager animationManager)
    {
        _animationManager = animationManager;
    }
    public void Enter(IEntity entity)
    {
        if (entity is PlayerController player)
        {
            _animationManager.PlayAnimationForState(this);
        }
    }

    public void Exit(IEntity entity)
    {
    }   

    public void FixedUpdate(IEntity entity)
    {
        if (entity is PlayerController player)
        {
            //them config moveSpeed sau
            player.Movement.Move(player.Input.HorizontalInput, speed);
        }
    }

    public void Update(IEntity entity)
    {
        if (entity is PlayerController player)
        {
            player.AnimationManager.PlayAnimationForState(this);
            
            if (player.Input.HorizontalInput == 0)
            {
                player.StateMachine.ChangeState(player.IdleState);
                return;
            }

            if (player.Input.JumpPressed)
            {
                player.StateMachine.ChangeState(player.JumpState);
                return ;
            }
            if (player.Input.AttackPressed)
            {
                player.StateMachine.ChangeState(player.AttackState);
                return;
            }


        }
    }
}
