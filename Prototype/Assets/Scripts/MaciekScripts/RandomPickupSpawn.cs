using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPickupSpawn : MonoBehaviour
{
    public GameObject labNote;
    public GameObject generatorPart;
    public Vector3[] spawnPoints;
    private List<int> RNG = new List<int>();


    void Start()
    {
        GenerateRandomOrder();
        SpawnPickups();
    }

    void GenerateRandomOrder()
    {
        int z = 0;
        while (z < spawnPoints.Length)
        {
            int x = Random.Range(0, spawnPoints.Length);
            if (!RNG.Contains(x))
            {
                RNG.Add(x);
                z++;
            }

        }

        foreach(int i in RNG)
        {
            Debug.Log(i);
        }
    }

    void SpawnPickups()
    {
        int labNotesCounter = 0;
        for (int i =0; i < spawnPoints.Length; i++)
        {
            if (labNotesCounter < 2)
            {
                Instantiate(labNote, spawnPoints[RNG[i]], Quaternion.identity);
            }
            else
            {
                Instantiate(generatorPart, spawnPoints[RNG[i]], Quaternion.identity);
            }
        }
    }
}
