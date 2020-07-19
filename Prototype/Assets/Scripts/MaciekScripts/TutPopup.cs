using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutEnemyPopup : MonoBehaviour
{
    public Transform cam;
    public Text enemyPopupText;
    public Image enemyPopupImage;

    [TextArea]
    public string[] enemyInformation;


    void Start()
    {
        
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
                        enemyPopupText.text = enemyInformation[0];
                        break;

                    case "DummyMediumX":
                        enemyPopupText.text = enemyInformation[1];
                        break;

                    case "DummyLargeX":
                        enemyPopupText.text = enemyInformation[2];
                        break;

                    case "Spawner":
                        break;

                    case "TutorialExit":
                        break;
                }
                enemyPopupImage.gameObject.SetActive(true);
            }
            else
            {
                enemyPopupImage.gameObject.SetActive(false);
            }

        }
    }
}
