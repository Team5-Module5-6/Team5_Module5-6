using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutPopup : MonoBehaviour
{
    public Transform cam;
    public Text popupText;
    public Image popupImage;

    //Script references
    private TutSpawner tutSpawnerScript;

    [TextArea]
    public string[] text;


    void Start()
    {
        tutSpawnerScript = FindObjectOfType<TutSpawner>();
    }

    void Update()
    {
        DetectEnemyDummy();
    }

    void DetectEnemyDummy()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(cam.position + cam.TransformDirection(Vector3.forward) * 0.5f, cam.TransformDirection(Vector3.forward), out raycastHit))
        {
            if (raycastHit.transform.gameObject.CompareTag("EnemyDummy")){

                switch (raycastHit.transform.gameObject.name)
                {
                    case "DummySmallX":
                        popupText.text = text[0];
                        break;

                    case "DummyMediumX":
                        popupText.text = text[1];
                        break;

                    case "DummyLargeX":
                        popupText.text = text[2];
                        break;

                    case "Spawner":
                        popupText.text = text[3];
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            tutSpawnerScript.SpawnTutEnemies();
                        }
                        break;

                    case "TutorialExit":
                        popupText.text = text[4];
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                            SceneManager.LoadScene(2);
                        }
                        break;
                }
                popupImage.gameObject.SetActive(true);
            }
            else
            {
                popupImage.gameObject.SetActive(false);
            }

        }
    }
}
