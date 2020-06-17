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
    //Script variables
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
    [Tooltip("Adjusts y coordinate to shoot rays at players height(Temporary untill enemies will have weapons that will rotate to shoot towards the player)")]
    public float yRayOffset;

    public void TakeDamage(int damageTaken) //easiest way of doing it, just call this function and type how much damage the enemy should take
    {
        health -= damageTaken;
        CheckHealth();
    }

    void CheckHealth()
    {
        if(health <= 0) 
        {
            //play death animation
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
