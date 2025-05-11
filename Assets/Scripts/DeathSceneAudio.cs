using UnityEngine;

public class DeathSceneAudio : MonoBehaviour
{
    public AudioSource deathSoundSource; // 🎶 AudioSource to play the death sound
    public AudioClip deathSound; // 💀 Death sound clip to play

    void Start()
    {
        // Check if the audio source and death sound are set
        if (deathSoundSource != null && deathSound != null)
        {
            deathSoundSource.PlayOneShot(deathSound); // 📢 Play the death sound
        }
        else
        {
            Debug.LogWarning("AudioSource or Death Sound not assigned.");
        }
    }
}
