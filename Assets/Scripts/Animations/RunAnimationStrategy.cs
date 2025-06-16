using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimationStrategy : IdleAnimationStrategy
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    public void Play(Animator animator)
    {
        animator.SetFloat(SpeedHash, 0);
    }
    public void Stop(Animator animator)
    {
        animator.SetFloat(SpeedHash, 0f);
    }
}
