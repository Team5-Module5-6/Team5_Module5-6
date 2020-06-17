using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public Transform[] weapons;
    public int currentWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        SwitchWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
            SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
            SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
            SwitchWeapon();
        }
    }

    void SwitchWeapon()
    {
        int i = 0;
        foreach(Transform weapon in weapons)
        {
            if (i == currentWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;

        }
    }
}
