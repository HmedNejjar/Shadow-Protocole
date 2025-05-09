using UnityEngine; // Import Unity engine functionalities

public class ShootSound : MonoBehaviour // Class to handle shooting and sound effects
{
    public float fireRate = 0.1f; // Time between shots (e.g., Glock ~0.1s, AK ~0.2s)
    public AudioClip shootSound; // Audio clip to play when shooting

    private float nextFireTime = 0f; // Tracks the next allowed time to fire
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to this GameObject
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime) // Check if the fire button is pressed and enough time has passed
        {
            Fire(); // Call the Fire method
        }
    }

    void Fire()
    {
        nextFireTime = Time.time + fireRate; // Set the next allowed fire time

        // Shoot logic here (e.g., raycast, projectile instantiation, etc.)

        if (shootSound != null) // Check if a shoot sound is assigned
        {
            audioSource.PlayOneShot(shootSound); // Play the shoot sound once
        }
    }
}