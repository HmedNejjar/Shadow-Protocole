using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthUpdate : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 500f; // 🔋 Max health value
    public float regenRate = 10f; // ♻️ Health points recovered per second
    public float regenDelay = 3f; // ⏱️ Time before regeneration starts after taking damage

    [Header("UI References")]
    public TextMeshProUGUI HealthText; // 🖥️ UI text element to display health

    [Header("Audio")]
    public AudioSource gameplayMusic; // 🎵 Reference to the gameplay music
    public AudioClip deathSound; // 💀 Sound to play on death
    private AudioSource audioSource; // 🎛️ Internal audio source for playing death sound

    private float health; // 🔢 Current health value
    private float timeSinceLastDamage; // ⏲️ Timer since last damage taken
    private bool hasDied = false; // ☠️ Flag to avoid multiple death triggers

    void Start()
    {
        health = maxHealth; // 🧬 Initialize health
        UpdateHealthUI(); // 🔄 Update health display on start

        audioSource = GetComponent<AudioSource>(); // 🔊 Get the attached AudioSource
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // ➕ Add AudioSource if missing
        }
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth); // 🛡️ Keep health within valid range

        // Regeneration logic
        if (timeSinceLastDamage < regenDelay)
        {
            timeSinceLastDamage += Time.deltaTime; // ⏳ Waiting before regen starts
        }
        else if (health < maxHealth)
        {
            health += regenRate * Time.deltaTime; // ➕ Regen health over time
        }

        UpdateHealthUI(); // 🔄 Keep UI updated

        // Death check
        if (health <= 0 && !hasDied)
        {
            hasDied = true; // ☑️ Prevent further death triggers
            Die(); // 💀 Handle death immediately
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage; // ➖ Apply damage
        timeSinceLastDamage = 0f; // 🔁 Reset regen delay timer
        health = Mathf.Max(health, 0); // 🔽 Clamp to zero minimum
        Debug.Log("Damage Taken. Current Health: " + health); // 🧾 Debug log
        UpdateHealthUI(); // 🔄 Reflect change in UI
    }

    void UpdateHealthUI()
    {
        if (HealthText != null)
            HealthText.text = $"Health: {Mathf.CeilToInt(health)}"; // 💬 Update UI text
    }

    void Die()
    {
        Debug.Log("Player has died!"); // ☠️ Log message

        if (gameplayMusic != null)
        {
            gameplayMusic.Stop(); // 🔇 Stop gameplay music
        }

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound); // 📢 Play death sound
        }

        Cursor.lockState = CursorLockMode.None;  // 🖱️ Unlock the mouse
        Cursor.visible = true; // 👁️ Make cursor visible

        SceneManager.LoadScene("DeathScene"); // 🎬 Load the death screen scene
    }

    public bool IsDead => health <= 0; // 🚨 Property to check if health is 0 or less
}
