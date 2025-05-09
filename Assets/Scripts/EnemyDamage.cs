using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damage = 20f; // Amount of damage the enemy deals to the player on touch
    public float damageInterval = 1f; // Time interval (in seconds) between consecutive damage

    private float lastDamageTime = 0f; // Tracks the last time damage was dealt

    private void OnTriggerStay(Collider other)
    {
        // Check if the object staying in the trigger is the player
        if (other.CompareTag("Player"))
        {
            HealthUpdate playerHealth = other.GetComponent<HealthUpdate>(); // Get the player's HealthUpdate component
            if (playerHealth != null && !playerHealth.IsDead) // Ensure the player has health and is not already dead
            {
                // Check if enough time has passed since the last damage
                if (Time.time >= lastDamageTime + damageInterval)
                {
                    playerHealth.TakeDamage(damage); // Apply damage to the player
                    lastDamageTime = Time.time; // Update the last damage time
                    Debug.Log("Enemy dealt " + damage + " damage to Player."); // Log the damage dealt
                }
            }
        }
    }
}