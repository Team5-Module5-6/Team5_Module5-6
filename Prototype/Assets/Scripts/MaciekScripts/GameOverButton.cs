//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 14/07/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//---Script Summary---\\
//Loads menu scene when GameOverButton is pressed
//
public class GameOverButton : MonoBehaviour
{
    public void GoToMenuScene() //Loads menu scene
    {
        SceneManager.LoadScene(2);
    }
}
