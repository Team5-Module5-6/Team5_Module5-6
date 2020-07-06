using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public Weapon[] weapons;
    public WeaponSwitch weaponSwitch;
    public TextMeshProUGUI weapon;
    public TextMeshProUGUI ammo;

    private int equippedWeapon;

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
    }
}
