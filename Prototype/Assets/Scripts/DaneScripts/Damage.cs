using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Affecting enemies with player projectiles
/// </summary>

    //This script is not used in the final product
public class Damage : MonoBehaviour
{
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
        if (other.gameObject.tag == "target")//If projectile hits an enemy
        {
            Destroy(other.gameObject);//Kill the enemy
            Destroy(gameObject);
        }
    }

}
