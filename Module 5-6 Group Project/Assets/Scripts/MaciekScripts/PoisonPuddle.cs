//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 25/06/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPuddle : MonoBehaviour
{
    //Script variables
    private float poisonDamageOverTime;
    private float poisonDamageOverTimeInterval;
    private int poisonTicksOfDamageOverTime;
    private float poisonSlowPercentage;
    private float poisonSlowDuration;
    private float tempSpeed;

    //Script references
    private EnemyStats enemyStatsScript;
    private PlayerStats playerStatsScript;
    private PlayerMovement playerMovementScript;



    void Start()
    {
        //Script references
        enemyStatsScript = FindObjectOfType<EnemyStats>();
        playerStatsScript = FindObjectOfType<PlayerStats>();
        playerMovementScript = FindObjectOfType<PlayerMovement>();

        //Script variables
        poisonDamageOverTime = enemyStatsScript.poisonDamageOverTime;
        poisonDamageOverTimeInterval = enemyStatsScript.poisonDamageOverTimeInterval;
        poisonTicksOfDamageOverTime = enemyStatsScript.poisonTicksOfDamageOverTime;
        poisonSlowPercentage = enemyStatsScript.poisonSlowPercentage;
        poisonSlowDuration = enemyStatsScript.poisonSlowDuration;
        tempSpeed = playerMovementScript.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(DamageOverTime(poisonTicksOfDamageOverTime, poisonDamageOverTimeInterval, poisonDamageOverTime));
            StartCoroutine(PlayerSlow(poisonSlowPercentage, poisonSlowDuration));
        }
    }

    IEnumerator DamageOverTime(int numberOfDamageTicks, float timeInterval, float damagePerTick)
    {
        for (int i = 0; i < numberOfDamageTicks; i++)
        {
            yield return new WaitForSeconds(timeInterval);
            playerStatsScript.TakeDamage(damagePerTick);
        }
    
    }
    
    IEnumerator PlayerSlow(float xslowPercentage, float xslowDuration)
    {
        playerMovementScript.speed *= xslowPercentage;
        yield return new WaitForSeconds(xslowDuration);
        playerMovementScript.speed = tempSpeed;
    }
}
