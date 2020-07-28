//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 24/07/2020

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//---Script Summary---\\
//Holds popup informations, allows for interaction with the popups, displays popups and enables UI elements for relevant popups
//

public class TutorialLogic : MonoBehaviour
{
    //Script variables
    public int ID = 0; //Determines what stage the player is at e.g. 0==tut start, 1==jumping, 2==crouch etc...
    public Text currentPopupText;
    public Image popupBackgroundImage;
    private bool isPopupActive;
    public PopupInformation[] numberOfPopups;

    //UI elements / objects that will be enabled as the player progresses in the tutorial
    public TextMeshProUGUI forceBlastCD, healingCD, health, pistolName, pistolAmmo;
    public Slider playerHealthBar;
    public GameObject tempGauge, meleeWeapon, primaryWeapon, secondaryWeapon, pushAbility, healingStation;

    //PopupInformation
    [System.Serializable] //Allows this class to be shown in the inspector
    public class PopupInformation
    {
        public string popupName; //Changes the name of corresponding element, used only for organisation in inspector
        public bool displayNextPopup; //Determines if there is a continuation of the popup
        [TextArea]
        public string TextToDisplay;
        public Vector3 popupPosition;
    }
    private void Start()
    {
        isPopupActive = popupBackgroundImage.IsActive(); 
        GenerateTutPopup();
    }

    private void Update() //Checks if the popup is active to enable/disable interactions with it
    {
        if (isPopupActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ToggleTutorialPopup(false);
                if(numberOfPopups[ID].displayNextPopup) //If there is a continuation to the popup, when player presses enter the next popup will show up
                {
                    ID++;
                    ToggleTutorialPopup(true);
                }
                
            }
        }
    }

    public void ToggleTutorialPopup(bool state) //Takes in bool value instead of alternating between true/false in case player won't close the popup before this method gets called again
    {
        //Debug.Log("yo " + ID.ToString());
        isPopupActive = state;
        if (isPopupActive)
        {
            EnableUIElements();
            GenerateTutPopup();
        }
        popupBackgroundImage.gameObject.SetActive(state);
    }

    private void GenerateTutPopup() //Sets the text and position of the UI element
    {
        currentPopupText.text = numberOfPopups[ID].TextToDisplay;
        popupBackgroundImage.rectTransform.anchoredPosition = numberOfPopups[ID].popupPosition;
    }

    private void EnableUIElements() //Holds information on which UI element to enable
    {
        switch (ID)
        {
            case 5: //Health
                playerHealthBar.gameObject.SetActive(true);
                health.gameObject.SetActive(true);
                break;

            case 6: //Pistol name and ammo
                primaryWeapon.gameObject.SetActive(true);
                secondaryWeapon.gameObject.SetActive(true);
                pistolAmmo.gameObject.SetActive(true);
                pistolName.gameObject.SetActive(true);
                meleeWeapon.gameObject.SetActive(true);
                break;

            case 7: //Temperature Gauge
                tempGauge.gameObject.SetActive(true);
                break;

            case 14: //Push ability
                forceBlastCD.gameObject.SetActive(true);
                pushAbility.gameObject.SetActive(true);
                break;

            case 15: //Healing ability
                healingCD.gameObject.SetActive(true);
                healingStation.gameObject.SetActive(true);
                break;
        }
    }
}
