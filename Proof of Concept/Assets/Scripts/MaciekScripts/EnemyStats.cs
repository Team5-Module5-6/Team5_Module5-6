﻿//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 17/06/2020

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyStats : MonoBehaviour
{
    //Script references
    private SpawnerV2 spawnerScript;
    private TemperatureGauge temperatureGaugeScript;
    private WaveHandler waveHandlerScript;

    //Script variables
    [Header("Basics")]
    [Tooltip("0 = EnemySmall\n1 = EnemyMedium\n2 = EnemyLarge")]
    public int enemyID;
    public int health;
    public float speed;
    [Tooltip("Distance at which the enemy will start to move towards the player")]
    public float triggerDistance;

    [Header("Shooting variables")]
    public int ammunition;
    [Tooltip("Projectiles per second")]
    public float rateOfFire;
    public float rangedAttackRange;
    public int rangedDamage;
    [Tooltip("Chance to hit in % e.g. 25 = 25% (Ranged Only)")]
    public int chanceToHit;

    [Header("Melee variables")]
    public int meleeDamage;
    public float meleeAttackRange;
    [Tooltip("Attacks per second")]
    public float meleeAttackRate;

    [Header("Fire StarStone variables (ID=1)")]
    [Header("StarStone effects variables")]

    public float damageOverTime;
    public float damageOverTimeInterval;
    public int ticksOfDamageOverTime;

    [Header("Ice StarStone variables (ID=2)")]
    [Tooltip("e.g. 0.2 = 20% slow")]
    public float slowPercentage;
    public float slowDuration;

    [Header("Poison StarStone variables (ID=3)")]
    public GameObject poisonPuddleObject;
    public float poisonDropInterval;
    public float poisonDamageOverTime;
    public float poisonDamageOverTimeInterval;
    public int poisonTicksOfDamageOverTime;
    public float poisonSlowPercentage;
    public float poisonSlowDuration;
    private Vector3 poisonPuddleSpawnOrigin;

    [Header("Electric StarStone variables (ID=4)")]
    public float stunDuration;

    [Header("other")]
    [Tooltip("Adjusts y coordinate to shoot rays at players height(Temporary fix until enemies will have weapons that will rotate towards the player)")]
    public float yRayOffset;

    private float enemyTemp;
    private int starStoneID;
    private float enemySizeY;
    private MeshRenderer enemyMeshRenderer;

    private void Start()
    {
        //Script References
        spawnerScript = FindObjectOfType<SpawnerV2>();
        temperatureGaugeScript = FindObjectOfType<TemperatureGauge>();
        waveHandlerScript = FindObjectOfType<WaveHandler>();

        //Mesh Rendered
        enemyMeshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();

        //Set variables
        starStoneID = waveHandlerScript.starStoneID;
        //enemySizeY = enemyMeshRenderer.bounds.size.y; //Doesn't work for some reason will look into it later

        switch (enemyID) //Used to adjust the spawn points of poison puddles to make sure theu are spawn below the enemy instead of inside of them
        {
            case 0:
                enemySizeY = 0.5f;
                break;

            case 1:
                enemySizeY = 1;
                break;

            case 2:
                enemySizeY = 2;
                break;
                
        }

        //Call functions
        GetTemperature();

        if(starStoneID == 3) //The way I think this is supposed to work is that the starstone type is chosen once every wave at random and the same starstone is used until the wave is finished
        {
            StartCoroutine(SpawnPoisonPuddles());
        }
    }


    public void TakeDamage(int damageTaken) //easiest way of doing it, just call this function and type how much damage the enemy should take
    {
        health -= damageTaken;
        CheckHealth();
    }

    void GetTemperature() //Gets temperature value of coresponding enemy type set in the spawner
    {
        enemyTemp = spawnerScript.enemyTemperature[enemyID];
    }

    void SubtractTemperature() //Cools down the generator when enemy is killed
    {
        temperatureGaugeScript.ChangeGeneratorTemperature(-enemyTemp); 
    }

    void CheckHealth()
    {
        if(health <= 0) 
        {
            //play death animation
            SubtractTemperature();
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnPoisonPuddles()
    {
        while(health > 0)
        {
            yield return new WaitForSeconds(poisonDropInterval);
            poisonPuddleSpawnOrigin = new Vector3(transform.position.x, transform.position.y - enemySizeY, transform.position.z);
            Instantiate(poisonPuddleObject, poisonPuddleSpawnOrigin, Quaternion.identity);            
        }
    }

    private void OnCollisionEnter(Collision collision) //Taking damage 
    {
        if (collision.gameObject.tag == "ObjectPlayerProjectiles")
        {
            TakeDamage(1);
        }
    }
}
