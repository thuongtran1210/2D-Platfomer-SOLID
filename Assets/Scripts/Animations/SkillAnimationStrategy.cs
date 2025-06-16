using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimationStrategy : BaseAnimationStrategy
{
    private static readonly int SkillHash = Animator.StringToHash("IsUsingSkill");
    private static readonly int SkillTypeHash = Animator.StringToHash("SkillType");
    private static readonly int SkillLevelHash = Animator.StringToHash("SkillLevel");

    private readonly int _skillIndex;
    private readonly float _skillDuration;

    public SkillAnimationStrategy(int skillIndex, float duration = 1.0f)
        : base($"Skill_{skillIndex}", duration)
    {
        _skillIndex = skillIndex;
        _skillDuration = duration;
    }

    public override void Play(Animator animator)
    {
        base.Play(animator);
        animator.SetTrigger(SkillHash);
        animator.SetInteger(SkillTypeHash, _skillIndex);
    }

    public override void Stop(Animator animator)
    {
        base.Stop(animator);
        animator.ResetTrigger(SkillHash);
    }

    public void SetSkillLevel(Animator animator, int level)
    {
        animator.SetInteger(SkillLevelHash, level);
    }
}
