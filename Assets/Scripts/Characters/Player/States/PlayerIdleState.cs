using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    
    private readonly AnimationManager _animationManager;
    public PlayerIdleState(AnimationManager animationManager)
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
    }

    public void Update(IEntity entity)
    {
        PlayerController player = entity as PlayerController;
        if (player == null) return;

        if (player.Input.AttackPressed) 
        {
            player.StateMachine.ChangeState(player.AttackState);
            return;
        }

        if (player.Input.Skill1Pressed) 
        {
            player.StateMachine.ChangeState(player.SkillState0);
            return;
        }
        if (player.Input.Skill2Pressed) 
        {
            player.StateMachine.ChangeState(player.SkillState1);
            return;
        }
        if (player.Input.Skill3Pressed) 
        {
            player.StateMachine.ChangeState(player.SkillState2);
            return;
        }


        if (player.Input.HorizontalInput != 0)
        {
            player.StateMachine.ChangeState(player.RunState);
        }
        if (player.Input.JumpPressed)
        {
            player.StateMachine.ChangeState(player.JumpState);
        }
    }
}
