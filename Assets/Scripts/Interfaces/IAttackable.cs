using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable 
{
    /// <summary>
    /// Attack Nomal.
    /// </summary>
    void PerformAttack();

    /// <summary>
    /// Use skill with skillIndex (0, 1, 2).
    /// </summary>
    /// <param name="skillIndex">Index skill (0, 1, 2).</param>
    void PerformSkill(int skillIndex);

    /// <summary>
    /// Check skill ready.
    /// </summary>
    /// <param name="skillIndex">Index skill.</param>
    bool IsSkillReady(int skillIndex);
}
