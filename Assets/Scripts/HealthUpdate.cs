using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthUpdate : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 500f;
    public float regenRate = 10f;
    public float regenDelay = 3f;

    [Header("UI References")]
    public TextMeshProUGUI HealthText;

    private float health;
    private float timeSinceLastDamage;
    private bool hasDied = false;

    void Start()
    {
        health = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        // Regeneration
        if (timeSinceLastDamage < regenDelay)
        {
            timeSinceLastDamage += Time.deltaTime;
        }
        else if (health < maxHealth)
        {
            health += regenRate * Time.deltaTime;
        }

        UpdateHealthUI();

        // Death check
        if (IsDead && !hasDied)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        timeSinceLastDamage = 0f;
        health = Mathf.Max(health, 0);
        Debug.Log("Damage Taken. Current Health: " + health);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (HealthText != null)
            HealthText.text = $"Health: {Mathf.CeilToInt(health)}";
    }

    void Die()
    {
        hasDied = true;
        Debug.Log("Player has died!");
        Cursor.lockState = CursorLockMode.None;  // Unlock the mouse
        Cursor.visible = true;
        SceneManager.LoadScene("DeathScene"); // Replace with your actual death scene name
    }

    public bool IsDead => health <= 0;
}
