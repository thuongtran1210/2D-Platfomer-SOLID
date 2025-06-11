using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMeleeAttack))]
[RequireComponent(typeof(PlayerSkillManager))]
public class PlayerAttack : MonoBehaviour, IAttackable
{
    private IMeleeAttack meleeAttacker;
      
    public enum AttackType { Melee, Ranged }
    [SerializeField] private AttackType currentAttackType = AttackType.Melee;
    private PlayerSkillManager skillManager;
    private void Awake()
    {
        meleeAttacker = GetComponent<IMeleeAttack>();

        if (meleeAttacker == null)
        {
            Debug.LogError("PlayerAttackController requires an IMeleeAttack component!");
            enabled = false;
        }
    }


    public void PerformAttack()
    {
        if (currentAttackType == AttackType.Melee)
        {
            if (meleeAttacker != null)
            {
                meleeAttacker.ExecuteMeleeAttack();
            }
            else
            {
                Debug.LogWarning("Cannot perform melee attack: No Melee Attacker assigned.");
            }
        }
    }
}
