using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public void GoToMenuScene()
    {
        SceneManager.LoadScene(2);
    }
}
