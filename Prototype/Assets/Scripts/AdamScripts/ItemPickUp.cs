using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public float pickUpDistance;
    public Inventory inventory;
    private Item itemFound;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position + transform.TransformDirection(Vector3.forward) * 0.5f, transform.TransformDirection(Vector3.forward), out raycastHit))
        {
            if (raycastHit.transform.gameObject.CompareTag("Item") && raycastHit.distance < pickUpDistance && raycastHit.distance > 0.5)
            {
                itemFound = raycastHit.transform.gameObject.GetComponent<Item>();

                if (itemFound.generatorPart)
                {
                    inventory.FoundPart();
                }
                if (itemFound.labNote)
                {
                    inventory.FoundNote();
                }

                Destroy(raycastHit.transform.gameObject);
            }
        }
    }
}
