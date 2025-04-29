using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 15f;
    public float regenDelay = 2f;

    [Header("UI Reference")]
    public Image staminaBar;

    [HideInInspector]
    public float currentStamina;

    private float timeSinceSprint;

    void Start()
    {
        currentStamina = maxStamina;
        UpdateStaminaUI();
    }

    void Update()
    {
        if (timeSinceSprint < regenDelay)
        {
            timeSinceSprint += Time.deltaTime;
        }
        else if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }

        UpdateStaminaUI();
    }

    public void UseStamina()
    {
        if (currentStamina > 0)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Max(0f, currentStamina);
            timeSinceSprint = 0f;
        }
    }

    public bool CanSprint()
    {
        return currentStamina > 10f; // Prevent edge-case stutter at low stamina
    }

    void UpdateStaminaUI()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = currentStamina / maxStamina;
        }
    }
}
