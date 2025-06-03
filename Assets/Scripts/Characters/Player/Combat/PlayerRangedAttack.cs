using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour, IRangedAttack
{
    [SerializeField] private GameObject projectilePrefab; // Prefab 
    [SerializeField] private Transform projectileSpawnPoint; // Start point 
    [SerializeField] private float defaultProjectileDamage = 10f;
    [SerializeField] private float defaultProjectileSpeed = 15f;
    [SerializeField] private float defaultProjectileRange = 10f;

    private float currentDamage;
    private float currentSpeed;
    private float currentRange;

    private void Awake()
    {
        currentDamage = defaultProjectileDamage;
        currentSpeed = defaultProjectileSpeed;
        currentRange = defaultProjectileRange;
    }
    public void ShootProjectile(Transform spawnPoint, Vector2 direction)
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab is not assigned in PlayerRangedAttack!");
            return;
        }

       
        GameObject newProjectileGO = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        Projectile newProjectile = newProjectileGO.GetComponent<Projectile>();

        if (newProjectile != null)
        {
            newProjectile.Initialize(currentDamage, currentSpeed, currentRange, direction, spawnPoint.position);
        }
        else
        {
            Debug.LogError("Projectile Prefab does not have a Projectile component!");
            Destroy(newProjectileGO);
        }

        // Add partical
       
    }

    public void ConfigureProjectile(float damage, float speed, float range)
    {
        this.currentDamage = damage;
        this.currentSpeed = speed;
        this.currentRange = range;
    }
    public void ExecuteRangedAttack(Vector2 direction)
    {
        direction = transform.right;

        ShootProjectile(projectileSpawnPoint, direction);
    }
}
