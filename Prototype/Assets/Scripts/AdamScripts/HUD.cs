using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public Weapon[] weapons;
    public WeaponSwitch weaponSwitch;
    public TextMeshProUGUI weapon;
    public TextMeshProUGUI ammo;
    public GameObject inventory;
    public Transform camera;
    public Slider enemyHealthPopUp;

    private int equippedWeapon;
    private float enemyMaxHealth;
    private float enemyHealth;

    void Update()
    {
        equippedWeapon = weaponSwitch.currentWeapon;

        switch (equippedWeapon)
        {
            case 0:
                weapon.text = "Melee";
                break;
            case 1:
                weapon.text = "Pistol";
                break;
            case 2:
                weapon.text = "Shotgun";
                break;
        }

        ammo.text = weapons[equippedWeapon].currentAmmo.ToString() + "/" + weapons[equippedWeapon].maxAmmo.ToString();

        if (Input.GetKey(KeyCode.I))
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }

        RaycastHit raycastHit;
        if (Physics.Raycast(camera.position + camera.TransformDirection(Vector3.forward) * 0.5f, camera.TransformDirection(Vector3.forward), out raycastHit))
        {
            if (raycastHit.transform.gameObject.CompareTag("Enemy"))
            {
                //enemyMaxHealth = raycastHit.transform.gameObject.GetComponent<EnemyStats>().maxHealth;
                enemyHealth = raycastHit.transform.gameObject.GetComponent<EnemyStats>().health;

                enemyHealthPopUp.transform.gameObject.SetActive(true);

                //enemyHealthPopUp.maxValue = enemyMaxHealth;
                enemyHealthPopUp.value = enemyHealth;
            }
            else
            {
                enemyHealthPopUp.transform.gameObject.SetActive(false);
            }
        }
    }
}
