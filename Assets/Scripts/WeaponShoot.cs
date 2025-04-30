using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public GameObject explosionEffect; // Explosion effect to instantiate when hitting an enemy
    public Transform gunBarrel; // Position of the gun's barrel (or muzzle)
    public float shootRange = 50f; // Max range of the shot
    public LayerMask enemyLayer; // Layer mask for enemies

    public bool isAK; // Determines if the current weapon is AK or pistol

    public float akFireRate = 0.1f; // Rate of fire for AK (how frequently it can shoot)
    private float nextTimeToFire = 0f; // Time to wait before the AK can shoot again

    public float pistolFireRate = 0.5f; // Time between pistol shots (cooldown)
    private float nextTimeToShoot = 0f; // Cooldown for pistol

    private void Update()
    {
        if (isAK)
        {
            // AK behavior: hold to shoot
            if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
            {
                ShootAK();
            }
        }
        else
        {
            // Pistol behavior: click to shoot
            if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToShoot)
            {
                ShootPistol();
            }
        }
    }

    // Handle AK fire (hold to shoot)
    void ShootAK()
    {
        // Create explosion effect at the gun barrel position
        GameObject explosion = Instantiate(explosionEffect, gunBarrel.position, Quaternion.identity);
        nextTimeToFire = Time.time + akFireRate; // Set the next time the AK can shoot
        FireProjectile();
        Destroy(explosion, 0.3f); // Destroy the explosion effect after 1 second
    }

    // Handle Pistol fire (click to shoot)
    void ShootPistol()
    {
        // Create explosion effect at the gun barrel position
        GameObject explosion = Instantiate(explosionEffect, gunBarrel.position, Quaternion.identity);
        nextTimeToShoot = Time.time + pistolFireRate; // Set the next time the pistol can shoot
        FireProjectile();
         Destroy(explosion, 0.3f); // Destroy the explosion effect after 1 second
    }

    void FireProjectile()
    {
        RaycastHit hit;
        if (Physics.Raycast(gunBarrel.position, gunBarrel.forward, out hit, shootRange, enemyLayer))
        {
            // Check if the raycast hits an enemy
            if (hit.collider.CompareTag("Enemy"))
            {
                // Apply damage to the enemy
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(20); // Change damage value as needed
                Debug.Log("Hit enemy: " + hit.collider.name);   

                // Instantiate explosion effect at hit point
               Instantiate(explosionEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
