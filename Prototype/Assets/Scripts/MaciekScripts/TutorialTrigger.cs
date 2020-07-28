//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 24/07/2020

using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

//---Script Summary---\\
//Checks for collision with triggers and displays popups corresponding with the area the player is in
//

public class TutorialTrigger : MonoBehaviour
{
    //Script references
    private TutorialLogic tutorialLogicScript;
    
    //Script variables
    public int triggerID; //Each triggerer has its ID to make sure that if player doesn't enter the trigger boxes in correct order...
                          //they will still get the appropriate popup to where they are
    void Start()
    {
        //Script reference
        tutorialLogicScript = FindObjectOfType<TutorialLogic>();
    }
    private void OnTriggerEnter(Collider other) //Checks collisions with the player
    {
        if(other.tag == "Player")
        {
            tutorialLogicScript.ID = triggerID; //Changes current ID to the one corresponding with collided trigger
            tutorialLogicScript.ToggleTutorialPopup(true); 
            Destroy(gameObject);                       
        }
    }
}
