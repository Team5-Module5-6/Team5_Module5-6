using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ammo box picking up and functioning
/// </summary>

public class Ammo : MonoBehaviour
{
    [Tooltip("Amount added to player ammo")]
    public int collectedAmmo = 10;//Ammount of ammo added to player's current ammo

    void OnTriggerEnter(Collider other)
    {
        Weapon ammo = other.GetComponent<Weapon>();
        if(ammo != null && ammo.currentAmmo < ammo.maxAmmo)//If collided with player/weapon and ammo is below max
        {
            ammo.AmmoUp(collectedAmmo);//Increase ammo via weapon script
            
            Destroy(gameObject);//Pick up ammo box
        }
    }
}
