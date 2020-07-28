//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 22/07/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//--Script Summary---\\
//Holds references to UI elements
//

public class HoldSSEffectInfo : MonoBehaviour //Only purpose of it is to hold references to UI elements that couldn't be referenced in enemy prefabs
{
    public Text ssEffectsText;
    public Image ssEffectsPopup;
}
