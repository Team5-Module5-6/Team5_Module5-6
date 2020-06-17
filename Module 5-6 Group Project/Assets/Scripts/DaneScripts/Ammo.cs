using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int collectedAmmo = 1;
 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Weapon ammo = other.GetComponent<Weapon>();
        if(ammo != null && ammo.currentAmmo < 100)
        {
            ammo.AmmoUp(collectedAmmo);
            
            
            Destroy(gameObject);
        }
    }
}
