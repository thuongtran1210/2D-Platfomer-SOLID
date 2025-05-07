using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IState
{
    public void Enter(IEntity entity)
    {
        if (entity is PlayerController player)
        {
            // Them config jumpForce sau
            player.Jump.Jump(10f);
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
        if (entity is PlayerController player)
        {
            player.StateMachine.ChangeState(new PlayerIdleState());
        }
    }
}
