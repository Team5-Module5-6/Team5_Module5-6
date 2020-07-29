using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;

    public GameObject pauseUI;
    public GameObject gameUI;
    public GameObject optionsUI;

    // Start is called before the first frame update
    void Start()
    {
        optionsUI.SetActive(false);
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        gameUI.SetActive(true);
        pauseUI.SetActive(false);
        optionsUI.SetActive(false);
        Time.timeScale = 1f;//Unfreeze game time
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;//Lock cursor to game view
    }

    void Pause()
    {
        gameUI.SetActive(false);
        pauseUI.SetActive(true);
        Time.timeScale = 0f;//Freeze game time
        paused = true;
        Cursor.lockState = CursorLockMode.None;//Unlock cursor to use for buttons
    }

    public void Options()
    {
        optionsUI.SetActive(true);
    }

    public void Menu()//Return to main menu
    {
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene(3);
    }
}
