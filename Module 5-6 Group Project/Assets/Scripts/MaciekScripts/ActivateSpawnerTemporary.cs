using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpawnerTemporary : MonoBehaviour
{
    private SpawnerV2 spawnerScript;
    void Start()
    {
        spawnerScript = FindObjectOfType<SpawnerV2>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { spawnerScript.SpawnerToggle(); }
    }
}
