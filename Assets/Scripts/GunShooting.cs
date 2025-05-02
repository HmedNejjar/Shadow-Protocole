using UnityEngine;

public class GunShooting : MonoBehaviour
{
    RaycastHit Hit;

    public enum WeaponType { AK, Pistol }
    
    [Header("Weapon Settings")]
    [SerializeField]
    private WeaponType weaponType;  // Now you can select weapon type from the Inspector
    
    [SerializeField]
    private Transform FirePoint;  // Point where the bullets are fired
    
    [SerializeField]
    private int currentAmmo;  // Ammo for the weapon
    
    [SerializeField]
    private float FireRate;  // Rate of fire for the weapon
    private float nextFire;  // Time when the weapon can shoot again
    
    [SerializeField]
    private float WeaponDamage;  // Damage dealt by the weapon
    
    [Header("Explosion Effect Settings")]
    [SerializeField]
    private GameObject explosionEffect;  // Reference to explosion effect prefab

    void Update()
    {
        if (weaponType == WeaponType.AK)
        {
            // AK behavior: hold to shoot
            if (Input.GetMouseButton(0) && currentAmmo > 0)
            {
                Shoot();
            }
        }
        else if (weaponType == WeaponType.Pistol && Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            // Pistol behavior: click to shoot
            Shoot();
        }
    }

    void Shoot()
    {
        
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;
            currentAmmo--;  // Decrease ammo on each shot

            // Instantiate explosion effect at the fire point
            if (explosionEffect != null)
            {
                GameObject effect = Instantiate(explosionEffect, FirePoint.position, Quaternion.identity);
                Destroy(effect, 0.3f);  // Destroy the explosion effect after 0.3 seconds
            }

            Debug.DrawRay(FirePoint.position, FirePoint.forward * 100f, Color.red);

            // Raycast to detect hits
            if (Physics.Raycast(FirePoint.position, FirePoint.forward, out Hit, 100f))
            {
                if (Hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Hit enemy! Applying damage.");
                    Hit.transform.GetComponentInParent<EnemyHealth>().TakeDamage(WeaponDamage);
                }
            }
        }
    }
}
