//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 22/07/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//---Script Summary---\\
//Shows instructions how to use the generator and enables interactions with it
//

public class ActivateSpawner : MonoBehaviour
{
    //LayerMasks
    private LayerMask generatorLayerMask;

    //Transform
    public Transform raycast;

    //Scripts
    private SpawnerV2 spawnerScript;
    private WaveHandler waveHandlerScript;
    private Prototype prototypeScript;//Dane

    //Images
    public Image generatorPopUp;

    //Variables
    public float range;
    private string sceneName;


    void Start()
    {
        //Layermask
        generatorLayerMask = LayerMask.GetMask("Generator");

        //ScriptReferences
        spawnerScript = FindObjectOfType<SpawnerV2>();
        waveHandlerScript = FindObjectOfType<WaveHandler>();
        prototypeScript = GameObject.Find("Prototype").GetComponent<Prototype>();//Dane

        //Scene name
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        DetectGenerator();
    }

    void DetectGenerator() //Casts a raycast to detect generator, when player is loking at it, it activated a popup and gives player the controls to activate it/change SS effects
    {

        if (Physics.Raycast(raycast.transform.position, raycast.transform.TransformDirection(Vector3.forward), range, generatorLayerMask))
        {
            if (!generatorPopUp.IsActive())
            {
                TogglePopUp();
            }
          
            if (Input.GetKeyDown(KeyCode.E) && !spawnerScript.isSpawnerOn)
            {
                if(sceneName != "Tutorial")//Disabled in tutorial as it's not needed
                {
                    spawnerScript.SpawnerToggle();
                }
            }

            if (Input.GetKeyDown(KeyCode.G) && !spawnerScript.isSpawnerOn)
            {
                waveHandlerScript.ChangeStarStone();
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
