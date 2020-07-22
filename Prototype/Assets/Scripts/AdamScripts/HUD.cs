using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public WeaponSwitch weaponSwitch;
    public TextMeshProUGUI weapon;
    public TextMeshProUGUI ammo;
    public GameObject inventory;
    public Transform cameraTransform;
    public Slider enemyHealthPopUp;

    private int equippedWeapon;
    private float enemyMaxHealth;
    private float enemyHealth;
    private List<Weapon> weapons = new List<Weapon>();

    private void Start()
    {
        for (int i = 0; i < weaponSwitch.weapons.Length; i++)
        {
            weapons.Add(weaponSwitch.weapons[i].GetComponent<Weapon>());
        }
    }

    void Update()
    {
        equippedWeapon = weaponSwitch.currentWeapon;

        switch (equippedWeapon)
        {
            case 0:
                weapon.text = "Melee";
                ammo.gameObject.SetActive(false);
                break;
            case 1:
                weapon.text = "Pistol";
                ammo.gameObject.SetActive(true);
                break;
            case 2:
                weapon.text = "Shotgun";
                ammo.gameObject.SetActive(true);
                break;
            case 3:
                weapon.text = "Prototype";
                ammo.gameObject.SetActive(false);
                break;
        }

        ammo.text = weapons[equippedWeapon].currentMag.ToString() + "/" + weapons[equippedWeapon].maxMag.ToString();

        if (Input.GetKey(KeyCode.I))
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
        
        RaycastHit raycastHit;
        if (Physics.Raycast(cameraTransform.position + cameraTransform.TransformDirection(Vector3.forward) * 0.5f, cameraTransform.TransformDirection(Vector3.forward), out raycastHit))
        {
            if (raycastHit.transform.gameObject.CompareTag("Enemy"))
            {
                enemyMaxHealth = raycastHit.transform.gameObject.GetComponent<EnemyStats>().maxHealth;
                enemyHealth = raycastHit.transform.gameObject.GetComponent<EnemyStats>().health;

                enemyHealthPopUp.transform.gameObject.SetActive(true);

                enemyHealthPopUp.maxValue = enemyMaxHealth;
                enemyHealthPopUp.value = enemyHealth;
            }
            else
            {
                enemyHealthPopUp.transform.gameObject.SetActive(false);
            }
        }
    }
}
