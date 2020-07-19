using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private TutorialLogic tutorialLogicScript;
    public int triggerID; //Each triggerer has its ID to make sure that if player doesn't enter the trigger boxes in correct order...
                          //they will still get the appropriate popup to where they are
    void Start()
    {
        tutorialLogicScript = FindObjectOfType<TutorialLogic>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            tutorialLogicScript.ID = triggerID; 
            tutorialLogicScript.ToggleTutorialPopup(true); 
            Destroy(gameObject);                         
        }
    }
}
