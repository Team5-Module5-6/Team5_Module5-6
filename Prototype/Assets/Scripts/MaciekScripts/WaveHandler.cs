//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 19/07/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//---Script Summary---\\
//Holds wave and star stone information that is used in other scripts, lose state, wave progression, star stone change and changes generator colour
//

public class WaveHandler : MonoBehaviour
{
    //Script variables
    [Tooltip("Current wave\n 0 = wave 1\n 1 = wave 2\n 2 = wave 3...")]
    public int waveID = 0;
    public int starStoneID = 0;

    //GameObjects
    public GameObject generator;

    //Renderer
    private Renderer generatorRenderer;

    //CharController
    private CharacterController playerCharController;

    //Script references
    private MouseLook mouseLookScript;

    //Button
    public Button gameOverButton;

    private void Start()
    { 
        //Script references
        mouseLookScript = GameObject.Find("Main Camera").GetComponent<MouseLook>();
        generatorRenderer = generator.GetComponent<Renderer>();

        //Char controller
        playerCharController = GameObject.Find("Player").GetComponent<CharacterController>();

        //UI element
        gameOverButton.gameObject.SetActive(false);
    }


    public void KillAllEnemies() //Destroys enemies
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject i in enemies) Destroy(i);
    }

    public void AdvanceToNextWave() //Advances to the next wave
    {
        waveID++;
    }

    public void ChangeStarStone() //Changes current star stone effect
    {
        if(starStoneID >= 0 && starStoneID <= 3) 
        {
            starStoneID++;
        }
        else
        {
            starStoneID = 1;
        }
        ChangeGeneratorColour();
    }

    void ChangeGeneratorColour() //Changes generator colour (player feedback)
    {
        switch (starStoneID)
        {
            case 1:
                generatorRenderer.material.color = Color.red;
                break;

            case 2:
                generatorRenderer.material.color = Color.cyan;
                break;

            case 3:
                generatorRenderer.material.color = Color.green;
                break;

            case 4:
                generatorRenderer.material.color = Color.yellow;
                break;
        }
    }

    public void ToggleCharControler() //Disables char controler, used for defeat state
    {
        playerCharController.enabled = !playerCharController.enabled;
    }

    public void PlayerLose() //Disables Player movement and enables mouse controls, displays defeat button that takes the player to the main menu
    {
        ToggleCharControler();
        Cursor.lockState = CursorLockMode.None;
        mouseLookScript.mouseSensitivity = 0; //disables camera rotation
        gameOverButton.gameObject.SetActive(true);
    }
}
