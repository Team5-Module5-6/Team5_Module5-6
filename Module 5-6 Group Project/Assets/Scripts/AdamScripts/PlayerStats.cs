using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public float Health = 100;

    public TextMeshProUGUI healthUI;

    private void Start()
    {
        healthUI.text = "Health: " + Mathf.Round(Health).ToString();
    }

    private void Update()
    {
        healthUI.text = "Health: " + Mathf.Round(Health).ToString();
    }
}
