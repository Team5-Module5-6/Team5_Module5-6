﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int maxAmmo = 5;
    public int currentAmmo;
    public Transform firePoint;
    public float fireForce = 60f;
    public GameObject projectilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && (currentAmmo > 0))
        {
            Shoot();
            //reduce ammo by one each shot
            currentAmmo = currentAmmo - 1;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * fireForce, ForceMode.VelocityChange);
    }

    
}
