using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    public void Enter(IEntity entity)
    {
      
      
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
            player.StateMachine.ChangeState(new PlayerAttackState());
            return;
        }

        if (player.Input.Skill1Pressed) 
        {
            player.StateMachine.ChangeState(new PlayerSkillState(0));
            return;
        }
        if (player.Input.Skill2Pressed) 
        {
            player.StateMachine.ChangeState(new PlayerSkillState(1));
            return;
        }
        if (player.Input.Skill3Pressed) 
        {
            player.StateMachine.ChangeState(new PlayerSkillState(2));
            return;
        }


        if (player.Input.HorizontalInput != 0)
        {
            player.StateMachine.ChangeState(new PlayerRunState());
        }
        if (player.Input.JumpPressed)
        {
            player.StateMachine.ChangeState(new PlayerJumpState());
        }
    }
}
