using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPuddleDespawn : MonoBehaviour
{
    private EnemyStats enemyStatsScript;
    private float despawnTime;


    void Start()
    {
        enemyStatsScript = FindObjectOfType<EnemyStats>();

        despawnTime = enemyStatsScript.poisonDespawnTime;

        StartCoroutine(DestroyPuddle());
    }

    IEnumerator DestroyPuddle()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

}
