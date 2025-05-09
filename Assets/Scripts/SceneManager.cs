using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // This method will be used to restart the game
    public void RestartGame()
    {
        // Replace "MainGame" with the name of your actual main game scene
        SceneManager.LoadScene("MainGame");
    }

    // Optional: If you want a method to quit the game (for a quit button)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
