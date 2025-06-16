using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimationStrategy
{
    void Play(Animator animator);
    void Stop(Animator animator);

}
