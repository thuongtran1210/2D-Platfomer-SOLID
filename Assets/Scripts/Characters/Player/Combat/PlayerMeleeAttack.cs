using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour, IMeleeAttack
{
    
    [SerializeField] private float meleeDamage = 20f;
    [SerializeField] private float attackRange = 1.5f; 
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform attackPoint;

    public void ExecuteMeleeAttack()
    {
        //  animation Meelee
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position
            + transform.right
            * (transform.localScale.x > 0 ? attackRange / 2 : -attackRange / 2)
            , attackRange / 2, enemyLayer); 
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            IDamageable damageable = enemyCollider.GetComponent<IDamageable>();
            if (damageable != null && damageable.IsAlive)
            {
                damageable.TakeDamage(meleeDamage);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Vector3 attackOrigin = attackPoint.position + transform.right * (transform.localScale.x > 0 ? attackRange / 2 : -attackRange / 2);
            Gizmos.DrawWireSphere(attackOrigin, attackRange / 2);
        }
    }

}
