using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAnimationStrategy : BaseAnimationStrategy
{
    private static readonly int JumpHash = Animator.StringToHash("IsJumping");
    private static readonly int JumpVelocityHash = Animator.StringToHash("JumpVelocity");
    public JumpAnimationStrategy() : base("Jump", 0.5f)
    {
    }
    public override void Play(Animator animator)
    {
        base.Play(animator);
        animator.SetBool(JumpHash, true);
    }

    public override void Stop(Animator animator)
    {
        base.Stop(animator);
        animator.SetBool(JumpHash, false);
        animator.SetFloat(JumpVelocityHash, 0f);
    }

    public void UpdateJumpVelocity(Animator animator, float velocity)
    {
        animator.SetFloat(JumpVelocityHash, velocity);
    }


}
