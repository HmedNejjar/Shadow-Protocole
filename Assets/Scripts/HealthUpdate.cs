using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthUpdate : MonoBehaviour
{
    [Header("Health Bar References")]
    public Image FrontHealth; // Reference to the front health bar UI
    public Image BackHealth; // Reference to the back health bar UI

    [Header("Health Settings")]
    public float maxHealth = 500f; // Maximum health value
    public float regenRate = 10f; // Health regeneration rate per second
    public float regenDelay = 3f; // Delay before health starts regenerating
    public float delaySpeed = 2f; // Speed of visual health bar update

    [Header("Death UI")]
    public GameObject deathPanel; // Reference to the death panel UI

    private float health; // Current health value
    private float visualHealth; // Visual representation of health for smooth UI updates
    private float timeSinceLastDamage; // Timer to track time since last damage
    private bool isDead = false; // Flag to check if the player is dead

    void Start()
    {
        health = maxHealth; // Initialize health to maximum
        visualHealth = maxHealth; // Initialize visual health to maximum
        UpdateHealthUI(); // Update the health bar UI

        if (deathPanel != null)
            deathPanel.SetActive(false); // Hide death panel at the start
    }

    void Update()
    {
        if (isDead) return; // Stop updating if the player is dead

        health = Mathf.Clamp(health, 0, maxHealth); // Clamp health between 0 and maxHealth

        // Health regeneration logic
        if (timeSinceLastDamage < regenDelay) // Check if regeneration delay has not passed
        {
            timeSinceLastDamage += Time.deltaTime; // Increment the timer
        }
        else if (health < maxHealth) // If health is below maximum
        {
            health += regenRate * Time.deltaTime; // Regenerate health over time
        }

        // Smoothly update the visual health bar
        visualHealth = Mathf.Lerp(visualHealth, health, Time.deltaTime * delaySpeed);
        UpdateHealthUI(); // Update the health bar UI

        if (health <= 0 && !isDead) // Check if health is zero and the player is not already dead
        {
            Die(); // Trigger death logic
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; // Ignore damage if the player is dead
        health -= damage; // Subtract damage from health
        timeSinceLastDamage = 0f; // Reset the regeneration delay timer
        health = Mathf.Max(health, 0); // Ensure health does not go below zero
    }

    void UpdateHealthUI()
    {
        FrontHealth.fillAmount = visualHealth / maxHealth; // Update the front health bar fill
        BackHealth.fillAmount = health / maxHealth; // Update the back health bar fill
        FrontHealth.color = health < maxHealth * 0.25f ? Color.red : Color.green; // Change color based on health level
    }

    void Die()
    {
        isDead = true; // Set the dead flag to true
        Time.timeScale = 0f; // Pause the game
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible

        if (deathPanel != null) // Check if the death panel is assigned
            deathPanel.SetActive(true); // Show the death panel

        Debug.Log("Player is ded :P"); // Log a message indicating the player is dead
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
