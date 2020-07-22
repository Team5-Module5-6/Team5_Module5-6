using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class CutsceneDialog : MonoBehaviour
{
    public TextMeshProUGUI dialog;
    private string[] sentences;
    [SerializeField]
    private int currentSentence;

    void Start()
    {
        sentences[0] = "This is dummy dialog asharaibibas rrabibibas bora porra aqui eh bodibuilder porra. Vai subir em arvore eh o caralho. BIRRLL !";
        sentences[1] = "This is Dummy dialog the second, son of dummy dialog ashraibibas rrabibibas bora porra.";
        sentences[2] = "This is dummY dialog the third, last of its kind cause i dont feel like making anymore will help.";

        currentSentence = 0;
    }

    void Update()
    {
        dialog.text = sentences[currentSentence];

        if (Input.GetMouseButtonDown(1))
        {
            currentSentence++;
        }
    }
}
