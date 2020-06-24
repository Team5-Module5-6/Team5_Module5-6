using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponHud : MonoBehaviour
{
    public Weapon[] weapons;
    public WeaponSwitch weaponSwitch;
    public TextMeshProUGUI equippedWeapon;
    public TextMeshProUGUI ammo;

    void Start()
    {
        
    }

    void Update()
    {
        ammo.text = weapons[0].currentAmmo.ToString() + "/" + weapons[0].maxAmmo.ToString();
    }
}
