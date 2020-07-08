using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public Slider healthUI;

    public WaveHandler waveHandler;

    private void Start()
    {
        healthUI.maxValue = maxHealth;
    }

    private void Update()
    {
        healthUI.value = currentHealth;

        if(currentHealth <= 0)
        {
            waveHandler.PlayerLose();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
