//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 17/06/2020

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Change raycast origin to enemy weapon
public class EnemyShooting : MonoBehaviour
{
    //Script references    
    private EnemyStats enemyStatsScript;

    //Script variables
    [HideInInspector]
    public bool playerInSight;

    private float range;
    private int hitChance;
    private float rateOfFire;
    private float yRayOffset;
    private bool isLoaded = true;
    
    //Coroutines
    private IEnumerator reloadCorutine;

    void Start()
    {   
        //Script references
        enemyStatsScript = GetComponent<EnemyStats>();

        //Corutines
        reloadCorutine = Reload();

        //Script variables
        range = enemyStatsScript.rangedAttackRange;
        hitChance = enemyStatsScript.chanceToHit;
        rateOfFire = enemyStatsScript.rateOfFire;
        yRayOffset = enemyStatsScript.yRayOffset;
    }

    void FixedUpdate()
    {
        ShootProjectile();
    }

    void ShootProjectile()
    {
        Vector3 rayOrigin = transform.position; //Determines where the ray is shot from, later when we have enemies with weapons, the rays will be shot from them
        rayOrigin.y += yRayOffset; //Adjusts ray height (temporary fix untill we get enemies with weapons)

        Ray sight = new Ray(rayOrigin, transform.forward); //Creates a ray that shoots in the direction the enemy is facing
        RaycastHit objHit; //Gets information on what got hit by the raycast

        if (Physics.Raycast(sight, out objHit, range)) 
        {
            if (objHit.transform.gameObject.name == "Player") {

                //Debug.Log("I can see you");
                playerInSight = true;

                if (isLoaded)
                {
                    //Play animation/effects
                    float hitChanceRNG = UnityEngine.Random.Range(0, 100); //Determines if the player got hit

                    if (hitChanceRNG <= hitChance)
                    {
                        //player.TakeDamage(value);
                        //Debug.Log("I shot you");
                    }
                    else
                    {
                        //Debug.Log("I missed");
                    }
                    isLoaded = false;
                    StartCoroutine(reloadCorutine); //Starts a coroutine to reload the gun
                }
            }
            else
            {
                //Debug.Log("I can't see you");
                playerInSight = false;
            }
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1 / rateOfFire); //Very bad way of determing rate of fire lmao
        isLoaded = true;
        StopCoroutine(reloadCorutine); //Not sure if it's needed, I think once it runs the code the coroutine stops itself but I'll leave it in for now

    }
}
