using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GunShooting : MonoBehaviour
{
    RaycastHit Hit;
    [Header("UI Elements")]
    [SerializeField]
    private TextMeshProUGUI ammoText;  // Reference to the UI text element for ammo count

    public enum WeaponType { AK, Pistol }
    
    [Header("Weapon Settings")]
    [SerializeField]
    private WeaponType weaponType;  // Now you can select weapon type from the Inspector
    
    [SerializeField]
    private Transform FirePoint;  // Point where the bullets are fired
    
    [SerializeField]
    private int akAmmo = 30;  // Ammo for AK

    [SerializeField]
    private int pistolAmmo = 15;  // Ammo for Pistol
    
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
        // Update the ammo text UI
        if (ammoText != null)
        {
            if (weaponType == WeaponType.AK)
            {
                ammoText.text = $"AK Ammo: {akAmmo}";
            }
            else if (weaponType == WeaponType.Pistol)
            {
                ammoText.text = $"Pistol Ammo: {pistolAmmo}";
            }
        }

        // Shooting logic
        if (weaponType == WeaponType.AK)
        {
            // AK behavior: hold to shoot
            if (Input.GetMouseButton(0) && akAmmo > 0)
            {
                ShootAK();
            }
        }
        else if (weaponType == WeaponType.Pistol)
        {
            // Pistol behavior: click to shoot
            if (Input.GetMouseButtonDown(0) && pistolAmmo > 0)
            {
                ShootPistol();
            }
        }
    }

    void ShootAK()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;
            akAmmo--;  // Decrease AK ammo
            HandleShooting();
        }
    }

    void ShootPistol()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;
            pistolAmmo--;  // Decrease Pistol ammo
            HandleShooting();
        }
    }

    void HandleShooting()
    {
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
