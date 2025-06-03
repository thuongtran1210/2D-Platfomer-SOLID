using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : ISkill
{
    protected SkillData _skillData;
    protected float _currentCooldown;

    public int SkillId => _skillData.skillId;
    public SkillData SkillData
    {
        get => _skillData;
        set
        {
            _skillData = value;
            _currentCooldown = 0f; // Khởi tạo cooldown khi SkillData được gán
        }
    }

    public virtual bool CanUseSkill()
    {
        return _currentCooldown <= 0;
    }

    public abstract void PerformSkill(IEntity entity);

    public virtual void UpdateSkill()
    {
        if (_currentCooldown > 0)
        {
            _currentCooldown -= Time.deltaTime;
        }
    }

    public float GetCurrentCooldown()
    {
        return _currentCooldown;
    }

    protected void ResetCooldown()
    {
        _currentCooldown = _skillData.cooldownTime;
    }
}
