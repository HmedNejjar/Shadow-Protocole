using UnityEngine;

public class GunSound : MonoBehaviour
{
    // Time between shots (e.g., Glock ~0.1 seconds, AK ~0.2 seconds)
    public float fireRate = 0.1f;

    // Audio clip to play when the gun is fired
    public AudioClip shootSound;

    // Tracks the next time the gun can fire
    private float nextFireTime = 0f;

    // Reference to the AudioSource component for playing sounds
    private AudioSource audioSource;

    // Called when the script is initialized
    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Called once per frame
    void Update()
    {
        // Check if the player is holding the fire button ("Fire1") and if the gun can fire
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Fire(); // Call the Fire method to handle shooting
        }
    }

    // Handles the firing logic
    void Fire()
    {
        // Set the next time the gun can fire based on the fire rate
        nextFireTime = Time.time + fireRate;

        // Play the shooting sound if a sound clip is assigned
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
