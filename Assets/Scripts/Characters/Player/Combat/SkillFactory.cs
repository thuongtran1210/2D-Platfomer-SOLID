using UnityEngine;
using System;

public class SkillFactory
{
    public static ISkill CreateSkill(SkillData skillData)
    {
        if (skillData == null)
        {
            Debug.LogError("Cannot create skill: SkillData is null");
            return null;
        }

        // Tạo skill dựa trên skillId hoặc skillName
        switch (skillData.skillId)
        {
            case 0: // Heal Skill
                return new HealSkill(skillData);
            case 1: // AoE Skill
                return new AoESkill(skillData);
            case 2: // Invisibility Skill
                return new InvisibilitySkill(skillData);
            default:
                Debug.LogError($"Unknown skill type with ID: {skillData.skillId}");
                return null;
        }
    }
} 