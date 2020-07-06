using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//First version of the spawner, won't be used

public class SpawnerV1 : MonoBehaviour
{
    [Tooltip("Time (in seconds) between enemy spawn")]
    public float spawnInterval;
    [Tooltip("Determines whether all enemies in a wave will spawn at one point or get scattered across other spawn points")]
    public bool spawnInOneArea = false;
    [Tooltip("0 == small / 1 == medium / 2 == large")]
    public GameObject[] enemies;
    [Tooltip("Coordinates of where the enemies will spawn")]
    public Vector3[] spawnPoints;
    public TwoDArray[] numberOfWaves;

    private int waveID;
    private WaveHandler waveHandlerScript;

    [HideInInspector]
    public int[] totalEnemiesInWave;

    private Vector3 spawnOrigin;

    [System.Serializable]
    public class TwoDArray
    {
        [HideInInspector]
        public string elementName;
        public int[] enemy;
    }

    void Start()
    {
        //Reference scripts
        waveHandlerScript = FindObjectOfType<WaveHandler>();

        //Set variables
        waveID = waveHandlerScript.waveID;
        totalEnemiesInWave = new int[numberOfWaves.Length];

        //Call ini functions
        CalcTotalEnemiesInEachWave();
    }


    void Update()
    {

    }

    IEnumerator SpawnEnemies()
    {
        if (spawnInOneArea)
        {
            DetermineSpawnOrigin();
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            for (int k = 0; k < numberOfWaves[waveID].enemy[i]; k++)
            {

                if (!spawnInOneArea)
                {
                    DetermineSpawnOrigin();
                }

                Instantiate(enemies[i], spawnOrigin, Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    void DetermineSpawnOrigin()
    {
        int spawnLocationRNG = Mathf.RoundToInt(Random.Range(0, spawnPoints.Length));
        spawnOrigin = spawnPoints[spawnLocationRNG];
    }

    public void ActivateSpawner()
    {
        StartCoroutine(SpawnEnemies());
    }

    void CalcTotalEnemiesInEachWave()
    {
        for (int i = 0; i < numberOfWaves.Length; i++)
        {
            for (int k = 0; k < enemies.Length -1; k++)
            {
                totalEnemiesInWave[i] += numberOfWaves[i].enemy[k];
            }
            //Debug.Log(totalEnemiesInWave[i]);
        }
    }

}
