using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimationStrategy : BaseAnimationStrategy
{
    private static readonly int IdleHash = Animator.StringToHash("IsIdle");

    public IdleAnimationStrategy() : base("Idle", 0f)
    {
    }

    public override void Play(Animator animator)
    {
        base.Play(animator);
        animator.SetBool(IdleHash, true);
    }

    public override void Stop(Animator animator)
    {
        base.Stop(animator);
        animator.SetBool(IdleHash, false);
    }

}
