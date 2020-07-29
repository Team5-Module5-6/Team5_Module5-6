//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 23/07/2020

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

//---Script summary---\\
//Randomly spawns pickups around the map
//

public class RandomPickupSpawn : MonoBehaviour
{
    //Game objects
    public GameObject labNote;
    public GameObject generatorPart;

    //Script variables
    public int numOfLabNotes, numOfGeneratoprParts;
    public Vector3[] spawnPoints;

    //List
    private List<int> RNG = new List<int>(); //Holds RNG to randomly determine spawn points of pickups


    void Start() //Called once at the start of the scene
    {
        GenerateRandomOrder();
        SpawnPickups();
    }

    void GenerateRandomOrder()//Generates a set of unique numbers, between 0 and number of spawn points in array, in random order to determnine where lab notes and generator parts will spawn
    {
        int z = 0;
        while (z < spawnPoints.Length) //Loops until there is a int generated for each spawn point
        {                              //Didn't want to use something from the internet so I came up with this, since it's only called once and amount of RNG is single didgit, it shouldn't be a problem
            int x = Random.Range(0, spawnPoints.Length);
            if (!RNG.Contains(x))
            {
                RNG.Add(x);
                z++;
            }

        }
        //foreach(int i in RNG)
        //{
        //    Debug.Log(i);
        //}
    }

    void SpawnPickups() //Spawns lab notes and generator parts
    {
        int labNotesCounter = 0;
        for (int i =0; i < spawnPoints.Length; i++) //Loops through all spawn points. checks for number of lab notes spawned and either spawns a lab note or a generator part
        {
            if (labNotesCounter < numOfLabNotes)
            {
                Instantiate(labNote, spawnPoints[RNG[i]], Quaternion.identity);  //Spawn point is determined by the order in which random numbers were generated in GenerateRandomOrder();
                labNotesCounter++;
            }
            else
            {
                Instantiate(generatorPart, spawnPoints[RNG[i]], Quaternion.identity);
            }
        }
    }
}
