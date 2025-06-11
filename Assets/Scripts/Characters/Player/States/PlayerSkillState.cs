using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : IState
{
    private int skillIndex;
    private float skillAnimationDuration = 1.0f; 
    private float skillTimer;
    public PlayerSkillState(int index)
    {
        this.skillIndex = index;
    }
    public void Enter(IEntity entity)
    {
        PlayerController player = entity as PlayerController;
        if (player == null) return;

        Debug.Log($"Entering PlayerSkillState for Skill Index {skillIndex}");

   
        if (player.SkillManager.IsSkillReady(skillIndex))
        {
            player.SkillManager.PerformSkill(skillIndex);
            skillTimer = skillAnimationDuration; 
           
        }
        else
        {
            Debug.Log($"Skill {skillIndex + 1} not ready, returning to Idle.");
            player.StateMachine.ChangeState(new PlayerIdleState()); 
        }
    }

    public void Exit(IEntity entity)
    {
        Debug.Log($"Exiting PlayerSkillState for Skill Index {skillIndex}");
    }

    public void FixedUpdate(IEntity entity)
    {
     
    }

    public void Update(IEntity entity)
    {
        PlayerController player = entity as PlayerController;
        if (player == null) return;

        skillTimer -= Time.deltaTime;

        if (skillTimer <= 0)
        {
            player.StateMachine.ChangeState(new PlayerIdleState());
        }
    }


}
