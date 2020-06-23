using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public Slider healthUI;

    private void Start()
    {
        healthUI.maxValue = maxHealth;
    }

    private void Update()
    {
        healthUI.value = currentHealth;
    }
}
