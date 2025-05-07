
using System.Diagnostics;

public class PlayerRunState : IState
{
    public void Enter(IEntity entity)
    {
        
    }

    public void Exit(IEntity entity)
    {
    }   

    public void FixedUpdate(IEntity entity)
    {
        if (entity is PlayerController player)
        {
            //them config moveSpeed sau
            player.Movement.Move(player.Input.HorizontalInput, 5f);
        }
    }

    public void Update(IEntity entity)
    {
        if (entity is PlayerController player)
        {
            if (player.Input.HorizontalInput == 0)
            {
                player.StateMachine.ChangeState(new PlayerIdleState());
            }

            if (player.Input.JumpPressed)
            {
                player.StateMachine.ChangeState(new PlayerJumpState());
            }
        }
    }
}
