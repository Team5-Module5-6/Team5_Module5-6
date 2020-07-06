//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 17/06/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleAttack : MonoBehaviour
{
    //Transform variables
    private Transform playerTransform;

    //Script references
    private EnemyStats enemyStatsScript;

    //Script variables
    private float attackRange;
    private float attackRate; //Determines time in s between each attack
    private bool attackReady = true;

    void Start()
    {
        //Transform
        playerTransform = GameObject.Find("Player").transform;

        //Script references
        enemyStatsScript = GetComponent<EnemyStats>();

        //Script variables
        attackRate = enemyStatsScript.meleeAttackRate;
        attackRange = enemyStatsScript.meleeAttackRange;
    }

    void Update() //Thats a very inefficient way of doing it, ima change it at some point 
    {
        CheckRange();
    }

    void CheckRange()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) <= attackRange) //Checks if player is within mele range
        {
            if(attackReady)
            {
                attackReady = false;
                MeleAttack();
                StartCoroutine(MeleAttackTimer());
            }
        }
    }

    IEnumerator MeleAttackTimer()
    {
        yield return new WaitForSeconds(1 / attackRate); //Converts to attacks per second rather than attack every x seconds
        attackReady = true;

    }

    void MeleAttack() //Up to Adam how we want the player to take dmg
    {
        //Play animation
        //player.TakeDamage(value);
        Debug.Log("I attacked");

    }
}
