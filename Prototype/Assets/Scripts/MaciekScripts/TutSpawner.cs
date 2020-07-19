using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TutSpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Vector3 spawnMiddle;
    public float spawnRadious;
    private Vector3 spawnPoint;

    public void SpawnTutEnemies()
    {
        DetermineSpawnPoint();

        int RNG = Random.Range(0, enemies.Length); //Determines which enemy is spawned
        Instantiate(enemies[RNG], spawnPoint, Quaternion.Euler(0,90,0));
    }

    void DetermineSpawnPoint()
    {
        float RNGx = Random.Range(-spawnRadious, spawnRadious);
        float RNGz = Random.Range(-spawnRadious, spawnRadious);
        spawnPoint = new Vector3(spawnMiddle.x + RNGx, spawnMiddle.y, spawnMiddle.z + RNGz);
    }

}
