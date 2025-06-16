using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimationStrategy : BaseAnimationStrategy
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");
    private static readonly int DirectionHash = Animator.StringToHash("Direction");


    public RunAnimationStrategy() : base("Run", 0f) // Duration = 0 vì là animation loop
    {
    }

    public override void Play(Animator animator)
    {
        base.Play(animator);
        animator.SetBool(IsRunningHash, true);
    }

    public override void Stop(Animator animator)
    {
        base.Stop(animator);
        animator.SetBool(IsRunningHash, false);
        animator.SetFloat(SpeedHash, 0f);
    }

    public void UpdateSpeed(Animator animator, float speed)
    {
        animator.SetFloat(SpeedHash, speed);
    }

    public void UpdateDirection(Animator animator, float direction)
    {
        animator.SetFloat(DirectionHash, direction);
    }
}
