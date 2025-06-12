using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerAttack;

public class CombatBlendTreeStrategy : IAnimationStrategy
{
    private const string BLEND_TREE_PARAM = "AttackType";
    private const string BLEND_TREE_NAME = "CombatBlendTree";
    public void PlayAnimation(Animator animator)
    {
        animator.SetBool("IsAttacking", true);
    }

    public void StopAnimation(Animator animator)
    {
        animator.SetBool("IsAttacking", false);
        animator.SetFloat(BLEND_TREE_PARAM, 0f);
    }

    public void UpdateBlendTree(Animator animator, float attackType)
    {
        animator.SetFloat(BLEND_TREE_PARAM, attackType);
    }

}
