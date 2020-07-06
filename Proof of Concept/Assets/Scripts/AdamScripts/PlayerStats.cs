using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Groups all player stats variables and methods for easy balancing
public class PlayerStats : MonoBehaviour
{
    // Health variables available in inspector for change
    public float maxHealth;
    public float currentHealth;

    // HUD UI reference
    public Slider healthUI;

    private void Start()
    {
        // Sets UI max value to health max value
        healthUI.maxValue = maxHealth;
    }

    private void Update()
    {
        // Sets UI current value to current health value
        healthUI.value = currentHealth;
    }

    // Method to remove specific amount of health from player as damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
