using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimationStrategy : IAnimationStrategy
{
    private static readonly int IdleHash = Animator.StringToHash("Idle");

    public void Play(Animator animator)
    {
        animator.SetBool("IsIdle", true);
    }

    public void Stop(Animator animator)
    {
        animator.SetBool("IsIdle", false);
    }

}
