//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 17/07/2020

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

//---Script Summary---\\
//Determines spawn chances of each enemy, randomly determines spawn position from given vectors, spawns a boss and enemies, toggles betwen active and inactive state, determines the temperature of spawned enemies to add to the generator,...
//holds information that determiens spawn chances and what enemeis are spawned in each wave
//

public class SpawnerV2 : MonoBehaviour
{
    //Script variables
    [Tooltip("Time (in seconds) between enemy spawn")]
    public float spawnInterval;
    public int enemiesSpawnPerInterval;
    [Tooltip("Determines whether all enemies in a wave will spawn at one point or get scattered across other spawn points")]
    public bool spawnInOneArea = false;
    [Tooltip("0 == small / 1 == medium / 2 == large")]
    public GameObject[] enemies;
    [Tooltip("Value that increases generators temperature")]
    public int[] enemyTemperature;
    [Tooltip("Cooling value multiplier (Enemy Temperature * this = cooling value)")]
    public float enemyCoolingMultiplier;
    [Tooltip("Coordinates of where the enemies will spawn")]
    public Vector3[] spawnPoints;
    [HideInInspector]
    public float[] spawnChance;

    private Vector3 spawnOrigin;
    private int waveID;
    [HideInInspector]
    public bool isSpawnerOn = false;

    //Custom 2D array
    public TwoDArray[] numberOfWaves;

    //Script references
    private WaveHandler waveHandlerScript;
    private TemperatureGauge tempGaugeScript;

    //Coroutines
    private IEnumerator spawnCoroutine;

    [System.Serializable] //Allows this class to be shown in the inspector
    public class TwoDArray
    {
        [HideInInspector]
        public string elementName = "Wave"; //Changes the name of the name element
        public int[] enemySpawnRatio;
        public bool spawnBoss;
    }

    void Start()
    {
        //Reference scripts
        waveHandlerScript = FindObjectOfType<WaveHandler>();
        tempGaugeScript = FindObjectOfType<TemperatureGauge>();

        //Set variables
        waveID = waveHandlerScript.waveID;
        spawnChance = new float[enemies.Length];

        //Coroutines
        spawnCoroutine = SpawnEnemies();
    }

    //The reason why I made the spawn chance this way is to allow our Designer to determine the spawn chances however they want; so if they want the spawn chances to add up to a certain % or if they want to determine them by ratios, they can do it
    //It also prevents bugs from happening as the script can take any values to determine the spawn chance
    //Only gets called once per wave
    void CalcSpawnChances() //Calculates the spawn chance for each enemy in current wave
    {
        Array.Clear(spawnChance, 0, spawnChance.Length); //Resets array values

        float totalSpawnChanceValue = 0; //Resets the denominator used to determine spawn chance
        try //Try makes sure that if an error occurs, rest of the code can carry on running, prevents the game from crashing and helps to debug
        {
            for (int k = 0; k < numberOfWaves[waveID].enemySpawnRatio.Length; k++) //Adds spawn ratios to get the denominator value
            {
                totalSpawnChanceValue += numberOfWaves[waveID].enemySpawnRatio[k]; //Loops through an array of spawn ratios and adds them to one variable
            }

            //Debug.Log(totalSpawnChanceValue);

            for (int k = 0; k < numberOfWaves[waveID].enemySpawnRatio.Length; k++) //Determines the spawn chance of each enemy in current wave
            {
                spawnChance[k] += numberOfWaves[waveID].enemySpawnRatio[k] / totalSpawnChanceValue; //Divides the spawn ratio to determine chance in % between 0.0 ~ 1.0
                //Debug.Log(spawnChance[k]);
            }
        }
        catch(Exception e) //Catches any exemptions and logs them
        {
            Debug.Log(e.Message);
        }

        try //Spawns the boss at the start of a wave
        {
            if (numberOfWaves[waveID].spawnBoss)
            {
                SpawnBoss();
            }

        }
        catch (Exception e)//Catches any exemptions and logs them
        {
            Debug.Log(e.Message);
            SpawnerToggle(); //Turns off the spawner if an error occurs
        }
    }

    //The idea behind the spawner is that the spawn chances are in sections on a numberline from 0.0 to 1.0, a random number is generated and the spawn chance of first enemy in the array is added to a variable that hold accumulated spawn chance
    //Then an if function checks if the current accumulated spawn chance is smaller or equal to the generated random number, if true, the enemy gets spawned, if false it adds the next spawn chance value to the accumulated spawn chance and checks it again
    //
    //Example: A = 10%, B = 40%, C = 50%      RNG = 0.47
    //
    // 0.0 ||___________|______________RNG->|_____|_______________________________________|| 1.0
    //     ||           |                   |     |                                       ||
    //     ||---A:10%---|----------B:40%----------|------------------C:50%----------------||                                    
    //        0.0 - 0.10         0.10 - 0.50                       0.50 - 1.00
    //
    //In the first loop, accumulated spawn chance is equal to 0.10 and the RNG is 0.47, RNG > accumulated spawn chance which means that RNG does not lie within A range of values, so enemy A is not spawned and the for function loops again adding the next spawn chance value
    //In the second loop the accumulated spawn chance is equal to 0.5, RNG < 0.5 which means that RNG lies within B range of values, is too big to lie in A ragne and too small to lie in C range so enemy B is spawned
    //
    //Doing it this way allows to have any number of enemies that we want to spawn and use any values to determine spawn chance without iterating the code 

    IEnumerator SpawnEnemies()
    {
        float linearSpawnChance = 0;
        float temperatureToAdd = 0;

        //Debug.Log("The wave has started");

        while (isSpawnerOn) //Script is running constantly while the spawner is on
        {
            try//Try makes sure that if an error occurs, rest of the code can carry on running, prevents the game from crashing and helps to debug
            {
                if (spawnInOneArea) //Determines spawn origin if we want all enemies to spawn in one place per interval
                {
                    DetermineSpawnOrigin();
                }

                for (int z = 0; z < enemiesSpawnPerInterval; z++) //Allows the script to spawn z number of enemies in one frame
                {
                    float RNG = Random.Range(0.0f, 1.0f); //Determines which enemy gets spawned

                    for (int i = 0; i < numberOfWaves[waveID].enemySpawnRatio.Length; i++) //Loops through the spawn ratios in the current wave
                    {
                        linearSpawnChance += spawnChance[i]; //Adds the spawn chance % each loop
                        
                        if (RNG <= linearSpawnChance) //Checks if the RNG generated is smaller than the current linearSpawnChance
                        {                             //If the RNG is <= to the linearSpawnChance it spawns the enemy
                            if (!spawnInOneArea) //Determines spawn origin each time an enemy is spawn if we want the enemies to spawn at multiple locations
                            {
                                DetermineSpawnOrigin(); 
                            }

                            Instantiate(enemies[i], spawnOrigin, Quaternion.identity); //Instantiates the enemy at chosen location
                            temperatureToAdd += enemyTemperature[i]; //Adds the temperature value of spawn enemy to a variable that later adds the temp to the generator (Adding temperature value each loop causes other functions to be called and crashes Unity LMAO)
                            linearSpawnChance = 0; //Resets linearSpawnChance
                            break; //Goes back to running the previous loop
                        }
                    }
                }                              
            }
            catch (Exception e)//Catches any exemptions and logs them
            {
                Debug.Log(e.Message);
                SpawnerToggle(); //Turns off the spawner if an error occurs
            }            
            tempGaugeScript.ChangeGeneratorTemperature(temperatureToAdd); //Adds accumulated temperature values to the generator
            temperatureToAdd = 0; //Resets temperature values
            yield return new WaitForSeconds(spawnInterval); //Wait x seconds and run the script (from the While loop) again
        }
    }

    void DetermineSpawnOrigin() //Choses a spawn point at random within spawn array limits
    {
        int spawnLocationRNG = Mathf.RoundToInt(Random.Range(0, spawnPoints.Length)); 
        spawnOrigin = spawnPoints[spawnLocationRNG];
    }

    void SpawnBoss()
    {
        DetermineSpawnOrigin();
        Instantiate(enemies[3], spawnOrigin, Quaternion.identity);

    }

    public void SpawnerToggle() //Changes between ON and OFF state
    {
        waveHandlerScript.KillAllEnemies(); //Kills all enemies (temporary for ease of life) 
        isSpawnerOn = !isSpawnerOn; 
        if (isSpawnerOn)
        {
            waveID = waveHandlerScript.waveID; //Gets current wave ID
            CalcSpawnChances();
            StartCoroutine(spawnCoroutine);
        }
        else
        {
            StopCoroutine(spawnCoroutine);
        }

        Debug.Log("Spawner ON = " + isSpawnerOn);
    }
}
