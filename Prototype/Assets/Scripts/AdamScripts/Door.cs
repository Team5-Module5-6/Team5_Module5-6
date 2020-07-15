using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    public float maxInteractDistance;
    public Transform cameraTransform;
    public Inventory inventory;
    public GameObject popUp;

    void Update()
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(cameraTransform.position + cameraTransform.TransformDirection(Vector3.forward) * 0.5f, cameraTransform.TransformDirection(Vector3.forward), out raycastHit))
        {
            if(raycastHit.transform.gameObject.CompareTag("Door") && raycastHit.distance < maxInteractDistance)
            {
                popUp.SetActive(true);
                popUp.GetComponentInChildren<TextMeshProUGUI>().text = "Door Locked    (Need Key)";

                if (inventory.hasKey)
                {
                    popUp.GetComponentInChildren<TextMeshProUGUI>().text = "Press E to Open Door";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        raycastHit.transform.gameObject.GetComponent<Animator>().Play("Door");
                        inventory.hasKey = false;
                    }
                }
            }
        }
    }
}
