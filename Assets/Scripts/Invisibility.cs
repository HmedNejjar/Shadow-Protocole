using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Invisibility : MonoBehaviour
{
    [Header("Ability Settings")]
    public float stopDuration = 5f; // Duration to stop the enemy
    public float cooldownDuration = 15f; // Cooldown duration before the ability can be used again
    public KeyCode abilityKey = KeyCode.E; // Key to activate the ability

    [Header("UI Elements")]
    public GameObject uiPanel; // The whole UI panel (optional, for showing/hiding)
    public Image uiImage; // A progress bar or icon to show cooldown progress
    public TextMeshProUGUI uiText; // The TextMeshProUGUI element displaying "E" or countdown seconds

    [Header("Enemy Reference")]
    public PlayerChasing enemy; // Reference to the enemy AI script

    private bool isOnCooldown = false; // Tracks whether the ability is on cooldown

    void Update()
    {
        // Check if the ability key is pressed and the ability is not on cooldown
        if (Input.GetKeyDown(abilityKey) && !isOnCooldown)
        {
            StartCoroutine(HandleAbility()); // Start the ability coroutine
        }
    }

    IEnumerator HandleAbility()
    {
        isOnCooldown = true; // Start cooldown
        enemy.SetPlayerVisible(false); // Stop the enemy
        UpdateUI($"Invisible: {stopDuration:F1}s", 0f); // Show "Stopped" message

        float t = stopDuration; // Start the stop duration countdown
        while (t > 0)
        {
            UpdateUI($"Invisible: {t:F1}s", 1 - (t / stopDuration)); // Update the UI with remaining stop time
            yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
            t -= 0.1f; // Decrease the timer
        }

        enemy.SetPlayerVisible(true); // Resume the enemy
        t = cooldownDuration; // Start the cooldown duration countdown
        while (t > 0)
        {
            UpdateUI($"Cooldown: {t:F1}s", 1 - (t / cooldownDuration)); // Update the UI with remaining cooldown time
            yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
            t -= 0.1f; // Decrease the timer
        }

        isOnCooldown = false; // Cooldown ends
        UpdateUI("E", 0f); // Reset the UI to show "E" for activation
    }

    void UpdateUI(string text, float fillAmount)
    {
        // Show the UI panel if it exists
        if (uiPanel != null) uiPanel.SetActive(true);

        // Update the UI text if it exists
        if (uiText != null) uiText.text = text; // Update the TextMeshProUGUI text

        // Update the UI image fill amount if it exists
        if (uiImage != null) uiImage.fillAmount = fillAmount;
    }
}