//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 19/07/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---Script Summary---\\
//Despawns poison puddles
//

public class PoisonPuddleDespawn : MonoBehaviour
{
    //Scripts references
    private EnemyStats enemyStatsScript;

    //Script variables
    private float despawnTime;


    void Start()
    {
        //Script references
        enemyStatsScript = FindObjectOfType<EnemyStats>();

        //Script variables
        despawnTime = enemyStatsScript.poisonDespawnTime;

        //Coroutines
        StartCoroutine(DestroyPuddle());
    }

    IEnumerator DestroyPuddle() //Waits x seconds and destroys the object
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

}
