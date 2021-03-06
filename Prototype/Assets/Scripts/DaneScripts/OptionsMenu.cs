﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This script contains all of the functions of the options menu 
/// </summary>

public class OptionsMenu : MonoBehaviour
{
    public GameObject soundSlider;
    public GameObject graphicsMenu;
    public GameObject gameplayMenu;
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public void Sound()
    {
        soundSlider.SetActive(true);
    }

    public void Graphics()
    {
        graphicsMenu.SetActive(true);
    }

    public void Gamepaly()
    {
        gameplayMenu.SetActive(true);
    }

    public void Return()
    {
        SceneManager.LoadScene(3);
    }
}
