using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;

    public GameObject optionsMenu;
    
    public GameObject difficultyMenu;

    public void Play()
    {
        SceneManager.LoadScene(0);
        //difficultyMenu.SetActive(true);
        //mainMenu.SetActive(false);
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Return()
    {
        mainMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
