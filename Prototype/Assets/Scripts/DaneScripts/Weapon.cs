using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// All functions and variables related to primary and secondary weapon
/// One script for all weapons makes it easy for the designer to understand
/// </summary>

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats Variables")]
    public float fireRate;
    public int range;
    public int damage;
    public int weaponType;//1= rapidfire, 0= single shots

    [Header("Ammo Variables")]
    public int maxAmmo;
    public int currentAmmo;//ammo available
    public int maxMag;
    public int currentMag;//ammo that is loaded
    public int reloadAmount;//amount of ammo per reload 

    public ParticleSystem muzzleFlash;

    void Start()
    {
        //Set variable defaults
        currentAmmo = maxAmmo;
        currentMag = maxMag;
    }

    // Update is called once per frame
    void Update()
    {
        //Left mouse button pressed, ammo is more than 0
        if (Input.GetMouseButtonDown(0) && currentMag > 0)
        {
            //Rapidfire
            if (weaponType == 1)
            {
                InvokeRepeating("Shoot", 0f, fireRate);//(Define fire rate here)
            }

            //Single shots
            else if (weaponType == 0)
            {
                Shoot();
            }           
        }

        //Stop rapidfire shooting
        else if (Input.GetMouseButtonUp(0) || currentMag <= 0)
        {
            CancelInvoke("Shoot");
        }

        //Defining the ammount of ammo that will be loaded into he weapon
        reloadAmount = maxMag - currentMag;

        if (currentAmmo < reloadAmount)
        {
            reloadAmount = currentAmmo;
        }

        //Only allow reloading if the player's magazine is not full
        if (Input.GetKeyDown(KeyCode.R) && currentMag < maxMag)
        {
            Reload(reloadAmount);
        }
    }

    //Shooting
    void Shoot()
    {
        muzzleFlash.Play();

        //Reduce ammo by one each shot
        currentMag = currentMag - 1;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            //If the player hits an enemy, deal damage to that enemy
            //Hit.transform ensures that this only affects the hit enemy instance
            EnemyStats target = hit.transform.GetComponent<EnemyStats>();
            if (target != null) 
            {
                target.TakeDamage(damage);
            }
        }
    }

    //Ammo and reloading
    void Reload(int reloadAmount)
    {
        //Insert the required ammount from the player's total ammo into their magazine 
        //This ensures the ammo reloaded is always relative to the player's total ammo
        if(currentAmmo > 0)
        {
            currentAmmo -= reloadAmount;

            currentMag = currentMag + reloadAmount;
        }
    }

    public void AmmoUp(int collectedAmmo)
    {
        //Increase total ammo by the value defined in the "Ammo" script 
        //This funcion is called when a ammo box is picked up
        //Using this method allows ammo boxes to have different vlaues
        if(currentAmmo < maxAmmo)
        {
            currentAmmo += collectedAmmo;

            //This statement prevents the player's ammo from exceeding it's maximum value
            if (currentAmmo > maxAmmo)
            {
                currentAmmo = maxAmmo;
            }

        }
        
    }
}
