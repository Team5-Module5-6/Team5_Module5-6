using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarStones : MonoBehaviour
{
    public Transform raycastTransform;

    enum starStones
    {
        Fire,
        Ice,
        Lightning,
        Poison
    }

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(raycastTransform.position, raycastTransform.TransformDirection(Vector3.forward), out raycastHit))
        {
            if(Input.GetKeyDown(KeyCode.E) && raycastHit.transform.gameObject.CompareTag(""))
            {
                
            }
        }
    }
}
