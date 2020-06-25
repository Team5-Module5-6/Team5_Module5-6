using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int maxAmmo = 5;
    public int currentAmmo;
    public int range;
    public int damage = 0;
    public float fireRate;

    public ParticleSystem muzzleFlash;

    void Start()
    {
        currentAmmo = maxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        //Left mouse button pressed, ammo is more than 0
        if (Input.GetMouseButtonDown(0) && (currentAmmo > 0))
        {
            //Rapidfire
            if (fireRate == 1)
            {
                InvokeRepeating("Shoot", 0f, 0.2f);
            }
            //Single shots
            else if (fireRate == 0)
            {
                Shoot();
            }           
        }
        //Stop shooting
        else if (Input.GetMouseButtonUp(0) && (currentAmmo > 0))
        {
            CancelInvoke("Shoot");
        }          
    }
    void Shoot()
    {
        muzzleFlash.Play();
        //Reduce ammo by one each shot
        currentAmmo = currentAmmo - 1;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            EnemyStats target = hit.transform.GetComponent<EnemyStats>();
            if (target != null) 
            {
                target.TakeDamage(damage);
            }
        }
    }

    public void AmmoUp(int collectedAmmo)
    {
        //increase ammo by selected value if current ammo is lower than the maxinum
        if(currentAmmo < maxAmmo)
        {
            currentAmmo += collectedAmmo;
            //don't allow current ammo to go over the set max value
            if (currentAmmo > maxAmmo)
            {
                currentAmmo = maxAmmo;
            }

        }
        
    }


}
