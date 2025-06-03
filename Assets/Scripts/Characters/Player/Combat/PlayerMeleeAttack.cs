using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour, IMeleeAttack
{
    
    [SerializeField] private float meleeDamage = 20f;
    [SerializeField] private float attackRange = 1.5f; 
    [SerializeField] private LayerMask enemyLayer;
    public void ExecuteMeleeAttack()
    {
        //  animation Meelee
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            transform.position
            + transform.right
            * (transform.localScale.x > 0 ? attackRange / 2 : -attackRange / 2)
            , attackRange / 2, enemyLayer); 
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            IDamageable damageable = enemyCollider.GetComponent<IDamageable>();
            if (damageable != null && damageable.IsAlive)
            {
                damageable.TakeDamage(meleeDamage);
                Debug.Log($"Dealt {meleeDamage} melee damage to {enemyCollider.name}.");
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (transform != null)
        {
            Gizmos.color = Color.red;
            Vector3 attackOrigin = transform.position + transform.right * (transform.localScale.x > 0 ? attackRange / 2 : -attackRange / 2);
            Gizmos.DrawWireSphere(attackOrigin, attackRange / 2);
        }
    }

}
