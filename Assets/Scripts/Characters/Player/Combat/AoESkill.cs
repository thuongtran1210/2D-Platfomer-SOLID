using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoESkill : BaseSkill
{
    [SerializeField] private float aoERadius = 5f; 
    [SerializeField] private float aoEDamage = 50f;

    public AoESkill(SkillData data) { SkillData = data; }
    public AoESkill(float radius, float damage, SkillData data)
    {
        this.aoERadius = radius;
        this.aoEDamage = damage;
        SkillData = data;
    }
    public override void PerformSkill(IEntity entity)
    {
        if (!CanUseSkill())
        {
            Debug.Log($"AoE Skill ({_skillData.skillName}) is on cooldown.");
            return;
        }

        Debug.Log($"Player ({entity.GetType().Name}) used AoE Skill. Affecting enemies within {aoERadius} radius for {aoEDamage} damage.");

       
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(entity.EntityTransform.position,aoERadius); 
        foreach (var hitCollider in hitColliders)
        {
            // Check player
            if (hitCollider.gameObject == (entity as MonoBehaviour)?.gameObject) continue;

            IDamageable target = hitCollider.GetComponent<IDamageable>();
            if (target != null && target.IsAlive)
            {
                // Check Enemy 
                if (hitCollider.CompareTag("Enemy"))
                {
                    target.TakeDamage(aoEDamage);
                    Debug.Log($"Dealt {aoEDamage} AoE damage to {hitCollider.name}.");
                }
            }
        }
        ResetCooldown();
    }
}
