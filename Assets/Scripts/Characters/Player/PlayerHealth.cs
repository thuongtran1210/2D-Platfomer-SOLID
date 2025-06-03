using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    public bool IsAlive => currentHealth > 0;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth; 

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0; 
        Debug.Log($"Took {damage} damage. Current HP: {currentHealth}");
        if (!IsAlive)
        {
            Debug.Log("Player died!");
            
        }
    }
    public void Heal(float amount) 
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth; 
        Debug.Log($"Healed for {amount}. Current HP: {currentHealth}");
    }
}
