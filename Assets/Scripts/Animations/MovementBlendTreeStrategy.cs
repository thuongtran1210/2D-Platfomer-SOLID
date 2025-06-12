using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBlendTreeStrategy : IAnimationStrategy
{
    private const string BLEND_TREE_PARAM = "Speed";
    private const string BLEND_TREE_NAME = "MovementBlendTree";
    public void PlayAnimation(Animator animator)
    {
        animator.SetBool("IsMoving", true);
    }

    public void StopAnimation(Animator animator)
    {
        animator.SetBool("IsMoving", false);
        animator.SetFloat(BLEND_TREE_PARAM, 0f);
    }

    public void UpdateBlendTree(Animator animator, float speed)
    {
        animator.SetFloat(BLEND_TREE_PARAM, speed);
    }
}
