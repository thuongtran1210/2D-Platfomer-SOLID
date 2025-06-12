using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationStrategy 
{
    void PlayAnimation(Animator animator);
    void StopAnimation(Animator animator);
    void UpdateBlendTree(Animator animator, float blendValue);

}
