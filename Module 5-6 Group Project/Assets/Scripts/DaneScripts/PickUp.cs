using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
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
        //Ammo pickUp = other.gameObject.GetComponent<Ammo>();
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);

        }
    }
}
