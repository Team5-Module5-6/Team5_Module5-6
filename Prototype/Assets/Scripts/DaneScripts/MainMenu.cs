using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;    
    public GameObject difficultyMenu;

    void Start()
    {
        optionsMenu.SetActive(false);
    }

    public void Play()
    {
        difficultyMenu.SetActive(true);//Go to difficulty selection
        mainMenu.SetActive(false);
    }

    public void Easy()
    {
        SceneManager.LoadScene(0);
    }

    public void Normal()
    {
        SceneManager.LoadScene(0);
    }

    public void Hard()
    {
        SceneManager.LoadScene(0);
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
