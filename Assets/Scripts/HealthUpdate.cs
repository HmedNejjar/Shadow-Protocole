using UnityEngine;
using UnityEngine.UI;

public class HealthUpdate : MonoBehaviour
{
    [Header("Health Bar References")]
    public Image FrontHealth;
    public Image BackHealth;

    [Header("Health Settings")]
    public float maxHealth = 500f;
    public float regenRate = 10f;
    public float regenDelay = 3f;
    public float delaySpeed = 2f;

    private float health;
    private float visualHealth; // Separate value for visual movement
    private float timeSinceLastDamage;

    void Start()
    {
        health = maxHealth;
        visualHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        // Handle regeneration
        if (timeSinceLastDamage < regenDelay)
        {
            timeSinceLastDamage += Time.deltaTime;
        }
        else if (health < maxHealth)
        {
            health += regenRate * Time.deltaTime;
        }

        // Smooth visual health transition
        visualHealth = Mathf.Lerp(visualHealth, health, Time.deltaTime * delaySpeed);

        // Print health to the console
        Debug.Log("Current Health: " + health);

        // Update the health bar UI
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        timeSinceLastDamage = 0f;
        health = Mathf.Max(health, 0);
    }

    void UpdateHealthUI()
    {
        // Front bar shows the exaggerated movement
        FrontHealth.fillAmount = visualHealth / maxHealth;

        // Back bar shows actual health (optional - can remove if not needed)
        BackHealth.fillAmount = health / maxHealth;

        FrontHealth.color = health < maxHealth * 0.25f ? Color.red : Color.green;
    }

    public bool IsDead => health <= 0;
}
