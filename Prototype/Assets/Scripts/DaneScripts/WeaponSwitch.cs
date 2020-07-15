using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public Transform[] weapons;
    //set default weapon here
    public int currentWeapon = 1;
    private int previousWeapon = 1;

    public float attackTime = 1f;
    public bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        SwitchWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            return;
        }
        else
        {
            //when not melee attacking, revert back to the previous weapon
            currentWeapon = previousWeapon;
            SwitchWeapon();
        }
            
        //change weapon on corresponding button press 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 1;
            previousWeapon = 1;
            SwitchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 2;
            previousWeapon = 2;
            SwitchWeapon();
        }
        //temporary
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentWeapon = 3;
            previousWeapon = 3;
            SwitchWeapon();
        }
        if (Input.GetMouseButtonDown(1))
        {
            currentWeapon = 0;
            SwitchWeapon();
            StartCoroutine(Attacking());
        
        }
    }

    IEnumerator Attacking()
    {
        isAttacking = true;

        yield return new WaitForSeconds(attackTime);
       
        isAttacking = false;
    }

    public void SwitchWeapon()
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
