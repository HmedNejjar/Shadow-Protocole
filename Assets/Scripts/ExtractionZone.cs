using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtractionZone : MonoBehaviour
{
    // UI element to display when the player wins
    public GameObject winUI;

    // Audio clip to play when the player wins
    public AudioClip winSound;

    // Audio source to play the win sound
    public AudioSource audioSource;

    // Trigger event when another collider enters the extraction zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Pause the game
            Time.timeScale = 0f;

            // Display the win UI
            winUI.SetActive(true);

            // Unlock and show the mouse cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Play the win sound if both the audio clip and source are assigned
            if (winSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(winSound);
            }
        }
    }

    // Method to restart the game by reloading the current scene
    public void RestartGame()
    {
        // Resume the game time
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
