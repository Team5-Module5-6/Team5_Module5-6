using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject soundSlider;

    public GameObject graphicsMenu;

    public GameObject gameplayMenu;


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
}
