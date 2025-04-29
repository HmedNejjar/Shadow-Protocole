using UnityEngine;

public class GunSwitch : MonoBehaviour
{
    public GameObject[] guns;
    public int currentGunIndex = 0; 
    void Start()
    {
        ActivateWeapon(currentGunIndex);
    }
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log("Scroll Input: " + scroll);

        if(scroll > 0f)
        {
            currentGunIndex = (currentGunIndex + 1) % guns.Length; // Next gun
            ActivateWeapon(currentGunIndex);
        Debug.Log("Current Gun Index: " + currentGunIndex);
        }
        else if (scroll < 0f)
        {
            currentGunIndex = (currentGunIndex - 1 + guns.Length) % guns.Length; // Previous gun
            ActivateWeapon(currentGunIndex);       
        }

    }

    void ActivateWeapon(int index)
{
    for (int i = 0; i < guns.Length; i++)
    {
        guns[i].SetActive(i == index);
        Debug.Log("Weapon " + i + " is now " + (i == index ? "ACTIVE" : "INACTIVE"));
    }
}

}
