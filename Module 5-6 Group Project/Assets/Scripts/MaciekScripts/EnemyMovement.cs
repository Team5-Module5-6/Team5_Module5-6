//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 17/06/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//Get rid of functions in fixed update and figure out a way to only call movement when needed

public class EnemyMovement : MonoBehaviour
{
    //Transform variables
    private Transform playerTransform;

    //NavMeshAgent
    private NavMeshAgent navigationAgent;

    //Script references
    private EnemyStats enemyStatsScript;
    private EnemyShooting enemyShootingScript;

    //Script variables
    private float triggerDistance;

    void Start()
    {
        //Transform
        playerTransform = GameObject.Find("Player").transform;

        //Script references
        navigationAgent = GetComponent<NavMeshAgent>();
        enemyStatsScript = GetComponent<EnemyStats>();
        enemyShootingScript = GetComponent<EnemyShooting>();

        //Script variables
        navigationAgent.speed = enemyStatsScript.speed;
        triggerDistance = enemyStatsScript.triggerDistance;
    }

    void FixedUpdate() //Fixed update as update every frame is not needed, saves on resources
    {
        MoveTowardsPlayer();
        RotateTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        //Debug.Log("mvment script.PlayerInSight: " + enemyShootingScript.playerInSight);

        if (Vector3.Distance(transform.position, playerTransform.position) <= triggerDistance || enemyShootingScript.playerInSight == false) //Checks if the player is within trigger distance or if anything is blocking enemies view of the player
        {
            navigationAgent.SetDestination(playerTransform.position); 
        }
        else
        {
            navigationAgent.ResetPath(); //Resets path to prevent the enemy from moving to the point last registered if the player is outside the trigger distance or view
        }
    }

    void RotateTowardsPlayer()
    {
        transform.LookAt(playerTransform.position); //Too difficult to explain                
    }
}
