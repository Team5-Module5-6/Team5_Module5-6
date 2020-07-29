//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 22/07/2020

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//---Script Summary---\\
//Spawns dummy enemies in set area in tutorial scene
//
public class TutSpawner : MonoBehaviour
{
    //Game Objects
    public GameObject[] enemies;

    //Script Variables
    public Vector3 spawnMiddle;
    public float spawnRadious;
    private Vector3 spawnPoint;

    public void SpawnTutEnemies()
    {
        DetermineSpawnPoint();

        int RNG = Random.Range(0, enemies.Length); //Determines which enemy is spawned
        Instantiate(enemies[RNG], spawnPoint, Quaternion.Euler(0,90,0));
    }

    void DetermineSpawnPoint() //Generates 2 random numbers within given radious to determine random spawn point
    {
        float RNGx = Random.Range(-spawnRadious, spawnRadious);
        float RNGz = Random.Range(-spawnRadious, spawnRadious);
        spawnPoint = new Vector3(spawnMiddle.x + RNGx, spawnMiddle.y, spawnMiddle.z + RNGz);
    }
}
