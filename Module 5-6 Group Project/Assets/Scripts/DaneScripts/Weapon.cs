using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int maxAmmo = 5;
    public int currentAmmo;
    public int range;
    public float fireRate;
    public float damage = 1f;

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
        //Reduce ammo by one each shot
        currentAmmo = currentAmmo - 1;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null) 
            {
                target.TakeDamage(damage);
            }
        }
    }

    public void AmmoUp(int collectedAmmo)
    {
        if(currentAmmo < 100)
        {
            currentAmmo += collectedAmmo;
        }
        
    }


}
