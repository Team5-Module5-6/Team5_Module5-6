//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 28/07/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---Script Summary---\\
//Deals melee damage to player when in melee range
//

public class EnemyMeleAttack : MonoBehaviour
{
    //GameObjects
    private GameObject player;

    //Transform variables
    private Transform playerTransform;

    //Script references
    private EnemyStats enemyStatsScript;
    private PlayerStats playerStatsScript;

    //Script variables
    private float attackRange;
    private float attackRate; //Determines time in s between each attack
    private bool attackReady = true;
    private float attackDamage;
    private float attackRatex;

    void Start()
    {
        //GameObjects
        player = GameObject.Find("Player");

        //Transform
        playerTransform = player.transform;

        //Script references
        enemyStatsScript = GetComponent<EnemyStats>();
        playerStatsScript = player.GetComponent<PlayerStats>();

        //Script variables
        attackRate = enemyStatsScript.meleeAttackRate;
        attackRange = enemyStatsScript.meleeAttackRange;
        attackDamage = enemyStatsScript.meleeDamage;
        attackRatex = 1 / attackRate;
    }

    void Update()
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

    IEnumerator MeleAttackTimer() //Cooldown between each attack
    {
        yield return new WaitForSeconds(attackRatex); 
        attackReady = true;

    }

    void MeleAttack() //Deals damage to player
    {
        //Play animation
        playerStatsScript.TakeDamage(attackDamage);
        //Debug.Log("I attacked");

    }
}
