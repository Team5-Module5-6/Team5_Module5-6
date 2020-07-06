//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 24/06/2020

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

    void DetectGenerator() //Casts a raycast and if the generator is hit displays a pop up with information how to activate it, I'm going to work with either Adam or Dane to implement this into one of their raycasts as I knjow they are also using them
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
