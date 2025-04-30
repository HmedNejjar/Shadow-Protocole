using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f; // Enemy's starting health
    
    public void TakeDamage(float damage) // Method to handle enemy damage
    {
        health -= damage; // Reduce enemy health by the given damage amount
        if (health <= 0f) // Check if enemy health reaches zero or below
        {
            Die(); // Call the Die method to handle enemy death
        }
    }
    
    public void Die() // Method to handle enemy death
    {
        Destroy(gameObject); // Destroy the enemy object when it dies
    }
}
