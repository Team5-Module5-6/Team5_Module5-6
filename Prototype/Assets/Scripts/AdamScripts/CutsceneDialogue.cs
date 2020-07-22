using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class CutsceneDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogue;
    public string nextScene;
    [TextArea(3, 10)]
    public List<string> sentences;
    private int currentSentence;

    void Update()
    {
        dialogue.text = sentences[currentSentence];
    }

    public void NextLine()
    {
        currentSentence++;
        if(currentSentence > sentences.Count)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
