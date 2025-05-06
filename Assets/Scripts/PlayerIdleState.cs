using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    public void Enter(IEntity entity)
    {
        Debug.Log("Idle");
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
            if (player.Input.HorizontalInput != 0)
            {
                player.StateMachine.ChangeState(new PlayerRunState());
            }
            else
            {
                player.Movement.Move(0, 0);
            }
            if (player.Input.JumpPressed)
            {
                //player.StateMachine.ChangeState(new PlayerJumpState());
            }
        }
    }
}
