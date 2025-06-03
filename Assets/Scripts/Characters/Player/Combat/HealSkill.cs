using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : BaseSkill
{
    [SerializeField] private float healAmount = 30f;

    // Contructor
    public HealSkill(SkillData data) { SkillData = data; }
    public HealSkill(float healAmount, SkillData data)
    {
        this.healAmount = healAmount;
        SkillData = data;
    }
    public override void PerformSkill(IEntity entity)
    {
        if (!CanUseSkill())
        {
            Debug.Log($"Heal Skill ({_skillData.skillName}) is on cooldown.");
            return;
        }

        if (entity.Healable != null)
        {
            entity.Healable.Heal(healAmount);
            Debug.Log($"Player ({entity.GetType().Name}) used Heal Skill. Healed for {healAmount}. Current HP: {entity.Healable.CurrentHealth}.");
            ResetCooldown();
        }
        else
        {
            Debug.LogWarning("User does not implement IDamageable for HealSkill.");
        }
    }
}
