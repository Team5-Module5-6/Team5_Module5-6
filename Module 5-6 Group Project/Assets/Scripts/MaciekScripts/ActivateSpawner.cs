using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ActivateSpawner : MonoBehaviour
{
    //LayerMasks
    private LayerMask generatorLayerMask;

    //Transform
    public Transform raycast;

    //Scripts
    private SpawnerV2 spawnerScript;

    //Images
    public Image generatorPopUp;

    //Variables
    public float range;


    void Start()
    {
        generatorLayerMask = LayerMask.GetMask("Generator");
        spawnerScript = FindObjectOfType<SpawnerV2>();
    }

    void Update()
    {
        DetectGenerator();
    }

    void DetectGenerator()
    {

        if (Physics.Raycast(raycast.transform.position, raycast.transform.TransformDirection(Vector3.forward), range, generatorLayerMask))
        {
            if (generatorPopUp.IsActive() == false)
            {
                TogglePopUp();
            }
          
            if (Input.GetKeyDown(KeyCode.E) && spawnerScript.isSpawnerOn == false)
            {
                spawnerScript.SpawnerToggle();
            }
        }
        else if (generatorPopUp.IsActive())
        {
            TogglePopUp();
        } 
    }

    void TogglePopUp() //Toggles between active and disabled state for the popup
    {
        generatorPopUp.gameObject.SetActive(!generatorPopUp.IsActive());
    }
}
