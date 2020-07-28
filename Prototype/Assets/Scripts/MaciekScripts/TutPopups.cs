//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 23/07/2020

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//---Script Summary---\\
//Displays popups and enables interaction with some objects in Tutorial scene
//

public class TutPopups : MonoBehaviour
{
    //Transform
    public Transform cam;

    //Text
    public Text popupText;

    //Image
    public Image popupImage;

    //Script references
    private TutSpawner tutSpawnerScript;

    //Script variables
    [TextArea]
    public string[] text;


    void Start()
    {
        //Script reference
        tutSpawnerScript = FindObjectOfType<TutSpawner>();
    }

    void Update()
    {
        DetectPopupObjects();
    }

    void DetectPopupObjects() //Checks if the player is looking at an object that has a popup attached to it in tutorial and enables interactions with some objects
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(cam.position + cam.TransformDirection(Vector3.forward) * 0.5f, cam.TransformDirection(Vector3.forward), out raycastHit))
        {
            if (raycastHit.transform.gameObject.CompareTag("EnemyDummy")){ //All interactable objects have an "EnemyDummy" tag as it was first designed to only show popups for enemies but later I used it for all new popups sctrictly for tutorial scene

                switch (raycastHit.transform.gameObject.name) //Checks for name of the object hit by the raycast
                {
                    case "DummySmallX":
                        popupText.text = text[0]; //Changes the text of the popup to match the object player is looking at
                        break;

                    case "DummyMediumX":
                        popupText.text = text[1];
                        break;

                    case "DummyLargeX":
                        popupText.text = text[2];
                        break;

                    case "Spawner":
                        popupText.text = text[3];
                        if (Input.GetKeyDown(KeyCode.E)) //Lets the player activate tutorial spawner to spawn dummy enemies
                        {
                            tutSpawnerScript.SpawnTutEnemies();
                        }
                        break;

                    case "TutorialExit":
                        popupText.text = text[4];
                        if (Input.GetKeyDown(KeyCode.E)) //Takes the player back to the main menu
                        {
                            Cursor.lockState = CursorLockMode.None; //Unlocks coursor controlls 
                            Cursor.visible = true;
                            SceneManager.LoadScene(2);
                        }
                        break;
                }
                popupImage.gameObject.SetActive(true); 
            }
            else
            {
                popupImage.gameObject.SetActive(false);
            }

        }
    }
}
