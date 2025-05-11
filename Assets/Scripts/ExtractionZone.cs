using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtractionZone : MonoBehaviour
{
    public GameObject winUI; // 🖥️ UI panel shown when the player wins
    public AudioClip winSound; // 🔊 Sound clip to play when the player wins
    public AudioSource audioSource; // 🎚️ Audio source to play the win sound
    public AudioSource gameplayMusic; // 🎵 Reference to background music

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ✅ Check if the collider belongs to the player
        {
            // Stop background music
            if (gameplayMusic != null)
            {
                gameplayMusic.Stop(); // 🔇 Stop the music when player enters extraction zone
            }

            // Show Win UI and pause game
            Time.timeScale = 0f; // ⏸️ Pause the game
            winUI.SetActive(true); // 📺 Show the win screen

            Cursor.lockState = CursorLockMode.None; // 🖱️ Unlock the mouse cursor
            Cursor.visible = true; // 👁️ Make the cursor visible

            if (winSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(winSound); // 🔊 Play the win sound once
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // ▶️ Resume game time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 🔁 Reload current scene
    }
}
