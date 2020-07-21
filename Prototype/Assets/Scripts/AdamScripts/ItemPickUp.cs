using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public float pickUpDistance;
    public Transform cameraTransform;
    public Inventory inventory;
    public GameObject popUp;

    private Item itemFound;

    void Update()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(cameraTransform.position + cameraTransform.TransformDirection(Vector3.forward) * 0.5f, cameraTransform.TransformDirection(Vector3.forward), out raycastHit))
        {
            if (raycastHit.transform.gameObject.CompareTag("Item") && raycastHit.distance < pickUpDistance)
            {
                itemFound = raycastHit.transform.gameObject.GetComponent<Item>();

                popUp.SetActive(true);
                popUp.GetComponentInChildren<TextMeshProUGUI>().text = "Press E to pick up " + itemFound.itemName;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (itemFound.current == Item.itemType.GeneratorPart)
                    {
                        inventory.FoundPart();
                    }
                    if (itemFound.current == Item.itemType.LabNote)
                    {
                        inventory.FoundNote();
                    }
                    if(itemFound.current == Item.itemType.Key)
                    {
                        inventory.hasKey = true;
                    }

                    Destroy(raycastHit.transform.gameObject);
                }
            }
        }
        
    }
}
