using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationStrategy : BaseAnimationStrategy
{
    private static readonly int AttackHash = Animator.StringToHash("IsAttacking");
    private static readonly int AttackTypeHash = Animator.StringToHash("AttackType");
    private static readonly int AttackComboHash = Animator.StringToHash("AttackCombo");
    public AttackAnimationStrategy() : base("Attack", 0.3f)
    {
    }
    public override void Play(Animator animator)
    {
        base.Play(animator);
        animator.SetTrigger(AttackHash);
    }

    public override void Stop(Animator animator)
    {
        base.Stop(animator);
        animator.ResetTrigger(AttackHash);
    }
    public void SetAttackType(Animator animator, int attackType)
    {
        animator.SetInteger(AttackTypeHash, attackType);
    }

    public void SetAttackCombo(Animator animator, int comboCount)
    {
        animator.SetInteger(AttackComboHash, comboCount);
    }

}
