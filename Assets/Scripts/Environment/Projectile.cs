using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private float speed;
    private float range;
    private Vector2 direction;
    private Vector2 spawnPosition;

    /// <summary>
    /// Khởi tạo đạn với các thông số cụ thể.
    /// </summary>
    /// <param name="damage">Sát thương của đạn.</param>
    /// <param name="speed">Tốc độ di chuyển của đạn.</param>
    /// <param name="range">Phạm vi tối đa của đạn.</param>
    /// <param name="direction">Hướng di chuyển của đạn.</param>
    /// <param name="spawnPosition">Vị trí ban đầu của đạn.</param>
    public void Initialize(float damage, float speed, float range, Vector2 direction, Vector2 spawnPosition)
    {
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        this.direction = direction.normalized; 
        this.spawnPosition = spawnPosition;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Check distance
        if (Vector2.Distance(spawnPosition, transform.position) >= range)
        {
            Destroy(gameObject); // Destroy if Projectile over range
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        // Check obj with tag
        
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Debug.Log($"Projectile hit {other.name} for {damage} damage.");
            }
            Destroy(gameObject); 
        }
    }
}
