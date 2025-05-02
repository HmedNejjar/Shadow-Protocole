using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public static event Action OnEnemyKilled;
    public float health = 100f; // Enemy's starting health
    
    public void TakeDamage(float damage) // Method to handle enemy damage
    {
        health -= damage; // Reduce enemy health by the given damage amount
        if (health <= 0f) // Check if enemy health reaches zero or below
        {
            Die(); // Call the Die method to handle enemy death
        }
    }

    public void Die()
{
    Destroy(gameObject);
    OnEnemyKilled?.Invoke();  // Notify the spawner
}

}
