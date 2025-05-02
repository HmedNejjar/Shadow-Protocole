using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerInvisibility : MonoBehaviour
{
    [Header("Invisibility Settings")]
    public float invisibilityDuration = 5f; // Duration of invisibility
    public float cooldownDuration = 15f;   // Cooldown time before invisibility can be used again
    public float invisibilitySpeedMultiplier = 2f; // Speed multiplier while invisible

    [Header("References")]
    public GameObject invisibilityPromptUI; // UI prompt to indicate invisibility availability
    public CanvasGroup invisibilityOverlay; // Visual overlay for invisibility effect
    public TextMeshProUGUI invisibilityText; // Text to display cooldown or status

    private bool isInvisible = false; // Tracks if the player is currently invisible
    private bool isOnCooldown = false; // Tracks if invisibility is on cooldown
    private float originalSpeed; // Stores the player's original speed

    private PlayerMotor movementScript; // Reference to the player's movement script
    private GunShooting gunScript; // Reference to the player's gun shooting script
    private InputManager inputManager; // Reference to the input manager

    void Start()
    {
        // Get references to required components
        movementScript = GetComponent<PlayerMotor>();
        gunScript = GetComponent<GunShooting>();
        inputManager = GetComponent<InputManager>();
        originalSpeed = movementScript.speed;

        // Initialize UI elements
        invisibilityOverlay.alpha = 0;
        invisibilityText.text = "E";

        // Bind the Invisibility action from the InputManager
        inputManager.playerInput.OnFoot.Invisibility.performed += _ => TryActivateInvisibility();
    }

    void Update()
    {
        // Update the invisibility prompt UI based on cooldown and invisibility status
        invisibilityPromptUI.SetActive(!isInvisible && !isOnCooldown);

        // Update cooldown text if on cooldown
        if (isOnCooldown)
        {
            invisibilityText.text = "Cooldown...";
        }
        else if (!isInvisible)
        {
            invisibilityText.text = "E";
        }
    }

    private void TryActivateInvisibility()
    {
        // Check if invisibility can be activated
        if (!isInvisible && !isOnCooldown)
        {
            StartCoroutine(HandleInvisibility());
        }
    }

    private System.Collections.IEnumerator HandleInvisibility()
    {
        Debug.Log("Invisibility activated");
        isInvisible = true;
        isOnCooldown = true;

        gunScript.enabled = false;
        movementScript.speed *= invisibilitySpeedMultiplier;

        invisibilityOverlay.alpha = 0.3f;
        gameObject.layer = LayerMask.NameToLayer("Invisible");

        yield return new WaitForSeconds(invisibilityDuration);

        Debug.Log("Invisibility ended");
        gunScript.enabled = true;
        movementScript.speed = originalSpeed;
        invisibilityOverlay.alpha = 0f;
        gameObject.layer = LayerMask.NameToLayer("Default");
        isInvisible = false;

        yield return new WaitForSeconds(cooldownDuration);
        Debug.Log("Cooldown ended");
        isOnCooldown = false;
    }
}
