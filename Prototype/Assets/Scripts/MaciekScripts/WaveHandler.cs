//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 29/06/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveHandler : MonoBehaviour
{
    //Script variables
    [Tooltip("Current wave\n 0 = wave 1\n 1 = wave 2\n 2 = wave 3...")]
    public int waveID = 0;
    public int starStoneID = 0;

    //GameObjects
    private GameObject generator;

    //Renderer
    private Renderer generatorRenderer;

    //CharController
    private CharacterController playerCharController;

    //Script references
    private MouseLook mouseLookScript;

    public Button gameOverButton;

    private void Start()
    {
        generator = GameObject.Find("Generator");
        mouseLookScript = GameObject.Find("Main Camera").GetComponent<MouseLook>();
        generatorRenderer = generator.GetComponent<Renderer>();

        //Char controller
        playerCharController = GameObject.Find("Player").GetComponent<CharacterController>();
        gameOverButton.gameObject.SetActive(false);
    }


    public void KillAllEnemies() //Just deletes them for now, will make the enemies take damage 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject i in enemies) Destroy(i);
    }

    public void AdvanceToNextWave() //Advances to the next wave
    {
        waveID++;
    }

    public void ChangeStarStone()
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

    void ChangeGeneratorColour()
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

    public void ToggleCharControler()
    {
        playerCharController.enabled = !playerCharController.enabled;
    }

    public void PlayerLose()
    {
        ToggleCharControler();
        Cursor.lockState = CursorLockMode.None;
        mouseLookScript.mouseSensitivity = 0;
        gameOverButton.gameObject.SetActive(true);
    }
}
