using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public float pickUpDistance;
    public Inventory inventory;
    public GameObject popUp;
    private Item itemFound;

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position + transform.TransformDirection(Vector3.forward) * 0.5f, transform.TransformDirection(Vector3.forward), out raycastHit))
        {
            if (raycastHit.transform.gameObject.CompareTag("Item") && raycastHit.distance < pickUpDistance)
            {
                itemFound = raycastHit.transform.gameObject.GetComponent<Item>();

                popUp.SetActive(true);
                popUp.GetComponentInChildren<TextMeshProUGUI>().text = "Press E to pick up " + itemFound.itemName;

                if (Input.GetKeyDown(KeyCode.E))
                {
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
            else
            {
                popUp.SetActive(false);
            }
        }
        
    }
}
