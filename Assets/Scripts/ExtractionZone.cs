using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExtractionZone : MonoBehaviour
{
    // Reference to the UI element that displays the win screen
    public GameObject winUI; 

    // Trigger event when another collider enters the extraction zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger zone has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Pause the game
            Time.timeScale = 0f;

            // Display the win UI
            winUI.SetActive(true);

            // Unlock and make the cursor visible for interaction with the UI
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Method to restart the game when called (e.g., from a button in the win UI)
    public void RestartGame()
    {
        // Resume the game time
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
