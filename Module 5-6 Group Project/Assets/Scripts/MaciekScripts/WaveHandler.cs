//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 17/06/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    //Script variables
    [Tooltip("Current wave\n 0 = wave 1\n 1 = wave 2\n 2 = wave 3...")]
    public int waveID = 0;

    public void KillAllEnemies() //Just deletes them for now, will make the enemies take damage 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject i in enemies) Destroy(i);
    }

    public void AdvanceToNextWave() //Advances to the next wave
    {
        waveID++;
    }
}
