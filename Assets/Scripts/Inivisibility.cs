using UnityEngine; // Import Unity engine functionalities
using UnityEngine.AI; // Import NavMeshAgent for controlling enemy movement
using TMPro; // Import TextMeshPro for UI text
using System.Collections; // Import for using Coroutines

public class Invisibility : MonoBehaviour // Class to handle invisibility ability
{
    public float invisDuration = 7f; // Duration of invisibility in seconds
    public float cooldownDuration = 15f; // Cooldown duration in seconds

    private float invisTimer = 0f; // Timer to track remaining invisibility time
    private float cooldownTimer = 0f; // Timer to track remaining cooldown time
    private bool isInvisible = false; // Flag to check if invisibility is active
    private bool onCooldown = false; // Flag to check if ability is on cooldown

    public TextMeshProUGUI uiText; // UI text to display status (assign in inspector)
    public CanvasGroup screenTint; // Screen overlay for visual feedback (assign in inspector)

    void Start()
    {
        uiText.text = "Press E"; // Initial UI text
        screenTint.alpha = 0f; // Set screen overlay to invisible at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isInvisible && !onCooldown) // Check if 'E' is pressed and ability is available
        {
            ActivateInvisibility(); // Activate invisibility
        }

        if (isInvisible) // If invisibility is active
        {
            invisTimer -= Time.deltaTime; // Decrease invisibility timer
            uiText.text = $"Invisible: {invisTimer:F1}s"; // Update UI text with remaining time
            if (invisTimer <= 0f) // If timer reaches zero
            {
                EndInvisibility(); // End invisibility
            }
        }
        else if (onCooldown) // If ability is on cooldown
        {
            cooldownTimer -= Time.deltaTime; // Decrease cooldown timer
            uiText.text = $"Cooldown: {cooldownTimer:F1}s"; // Update UI text with remaining cooldown
            if (cooldownTimer <= 0f) // If cooldown timer reaches zero
            {
                onCooldown = false; // Reset cooldown flag
                uiText.text = "Press E"; // Update UI text
            }
        }
    }

    void ActivateInvisibility()
    {
        isInvisible = true; // Set invisibility flag
        invisTimer = invisDuration; // Reset invisibility timer

        // Stop all enemies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) // Find all enemies by tag
        {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>(); // Get NavMeshAgent component
            if (agent != null) agent.isStopped = true; // Stop enemy movement
        }

        // Start fade-in effect for screen overlay
        StartCoroutine(FadeScreen(0f, 0.3f, 0.5f)); // Fade from transparent to semi-transparent
    }

    void EndInvisibility()
    {
        isInvisible = false; // Reset invisibility flag
        onCooldown = true; // Set cooldown flag
        cooldownTimer = cooldownDuration; // Reset cooldown timer

        // Resume all enemies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) // Find all enemies by tag
        {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>(); // Get NavMeshAgent component
            if (agent != null) agent.isStopped = false; // Resume enemy movement
        }

        // Start fade-out effect for screen overlay
        StartCoroutine(FadeScreen(0.3f, 0f, 0.5f)); // Fade from semi-transparent to transparent
    }

    IEnumerator FadeScreen(float from, float to, float duration)
    {
        float timer = 0f; // Timer to track fade progress
        while (timer < duration) // Loop until fade duration is complete
        {
            timer += Time.deltaTime; // Increment timer
            float alpha = Mathf.Lerp(from, to, timer / duration); // Interpolate alpha value
            screenTint.alpha = alpha; // Set screen overlay alpha
            yield return null; // Wait for the next frame
        }
        screenTint.alpha = to; // Ensure final alpha value is set
    }
}
