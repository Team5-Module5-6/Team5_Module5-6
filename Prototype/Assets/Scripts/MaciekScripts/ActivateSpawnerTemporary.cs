using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Temporary way of activating the spawner, prevents us from running to and from the generator
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
