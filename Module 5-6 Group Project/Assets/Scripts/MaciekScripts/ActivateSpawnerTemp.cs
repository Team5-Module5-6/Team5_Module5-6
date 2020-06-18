using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpawnerTemp : MonoBehaviour
{
    private SpawnerV2 spawnerScript;

    void Start()
    {
        spawnerScript = FindObjectOfType<SpawnerV2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { spawnerScript.SpawnerToggle(); }
    }
}
