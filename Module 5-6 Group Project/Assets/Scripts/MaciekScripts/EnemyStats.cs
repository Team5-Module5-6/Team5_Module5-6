//Author: Maciej Dowbor
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

    //Script variables
    [Tooltip("0 = EnemySmall\n1 = EnemyMedium\n2 = EnemyLarge")]
    public int EnemyID;
    public int health;
    public float speed;
    [Tooltip("Distance at which the enemy will start to move towards the player")]
    public float triggerDistance;
    public int ammunition;
    [Tooltip("Projectiles per second")]
    public float rateOfFire;
    public float rangedAttackRange;
    public int rangedDamage;
    [Tooltip("Chance to hit in % e.g. 25 = 25% (Ranged Only)")]
    public int chanceToHit;
    public int meleeDamage;
    public float meleeAttackRange;
    [Tooltip("Attacks per second")]
    public float meleeAttackRate;
    [Tooltip("Adjusts y coordinate to shoot rays at players height(Temporary fix until enemies will have weapons that will rotate towards the player)")]
    public float yRayOffset;

    private float enemyTemp;

    private void Start()
    {
        //Script References
        spawnerScript = FindObjectOfType<SpawnerV2>();
        temperatureGaugeScript = FindObjectOfType<TemperatureGauge>();
        GetTemperature();
    }

    public void TakeDamage(int damageTaken) //easiest way of doing it, just call this function and type how much damage the enemy should take
    {
        health -= damageTaken;
        CheckHealth();
    }

    void GetTemperature() //Gets temperature value of coresponding enemy type set in the spawner
    {
        enemyTemp = spawnerScript.enemyTemperature[EnemyID];
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

    //Function to apply StarStones bonuses <- Don't know what the bonuses are yet

    private void OnCollisionEnter(Collision collision) //Taking damage 
    {
        if (collision.gameObject.tag == "ObjectPlayerProjectiles")
        {
            TakeDamage(1);
        }
    }
}
