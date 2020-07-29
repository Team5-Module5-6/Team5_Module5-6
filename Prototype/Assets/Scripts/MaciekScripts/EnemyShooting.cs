//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 20/07/2020

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

//---Script Summary---\\
//Checks if player is in sight, shoots player, activates star stone effects on player and displays ss effects popup to inform player that they're for e.g. slowed
//
public class EnemyShooting : MonoBehaviour
{
    //SS effects player feedback
    private Text ssEffectsText;
    private Image ssEffectsPopup;

    //Particle Effect
    public ParticleSystem gunFlashEffect;
    public Transform gunTip;

    //Script references
    private EnemyStats enemyStatsScript;
    private PlayerStats playerStatsScript;
    private PlayerMovement playerMovementScript;
    private WaveHandler waveHandlerScript;
    private HoldSSEffectInfo ssPupupInfoScript;

    //Script variables
    [HideInInspector]
    public bool playerInSight;
    private int starStoneID = 0;
    private float range;
    private int hitChance;
    private float rateOfFire;
    private float yRayOffset;
    private bool isLoaded = true;
    private float rangedDamage;
    private float tempSpeed;
    private float rateOfFireCD;

    //Fire SS variables
    private float damageOverTime;
    private float damageOverTimeInterval;
    private int ticksOfDamageOverTime;

    //Ice SS variables
    private float slowPercentage;
    private float slowDuration;

    //Electric SS variables
    private float stunDuration;

    void Start()
    {   
        //Script references
        enemyStatsScript = GetComponent<EnemyStats>();
        playerStatsScript = FindObjectOfType<PlayerStats>();
        playerMovementScript = FindObjectOfType<PlayerMovement>();
        waveHandlerScript = FindObjectOfType<WaveHandler>();
        ssPupupInfoScript = FindObjectOfType<HoldSSEffectInfo>();

        //Script variables
        starStoneID = waveHandlerScript.starStoneID;
        range = enemyStatsScript.rangedAttackRange;
        hitChance = enemyStatsScript.chanceToHit;
        rateOfFire = enemyStatsScript.rateOfFire;
        rangedDamage = enemyStatsScript.rangedDamage;
        yRayOffset = enemyStatsScript.yRayOffset;
        tempSpeed = playerMovementScript.speed;
        rateOfFireCD = 1 / rateOfFire;

        //Fire StarStone
        damageOverTime = enemyStatsScript.damageOverTime;
        damageOverTimeInterval = enemyStatsScript.damageOverTimeInterval;
        ticksOfDamageOverTime = enemyStatsScript.ticksOfDamageOverTime;

        //Ice StarStone
        slowPercentage = enemyStatsScript.slowPercentage;
        slowDuration = enemyStatsScript.slowDuration;

        //Electric StarStone
        stunDuration = enemyStatsScript.stunDuration;

        //SS popup
        ssEffectsText = ssPupupInfoScript.ssEffectsText;
        ssEffectsPopup = ssPupupInfoScript.ssEffectsPopup;
    }

    void FixedUpdate()
    {
        ShootProjectile();
    }

    void ShootProjectile() //Damages player and determines if the player is in front of the enemy, combined both into one as the script only runs to the if(isloaded) and shouldn't have big impact if it's ran at the same rate as checking if player's in sight
    {
        Vector3 rayOrigin = transform.position; //Determines where the ray is shot from, later when we have enemies with weapons, the rays will be shot from them
        rayOrigin.y += yRayOffset; //Adjusts ray height (temporary fix untill we get enemies with weapons)

        Ray sight = new Ray(rayOrigin, transform.forward); //Creates a ray that shoots in the direction the enemy is facing
        RaycastHit objHit; //Gets information on what got hit by the raycast
        //Debug.DrawRay(rayOrigin, transform.forward, Color.blue);


        if (Physics.Raycast(sight, out objHit, range)) 
        {
            if (objHit.transform.gameObject.tag == "Player") {

                //Debug.Log("I can see you");
                playerInSight = true;
                //Debug.Log("Is loaded: " + isLoaded);

                if (isLoaded)
                {
                    //Play animation/effects
                    Instantiate(gunFlashEffect, gunTip.position, gunTip.rotation);

                    float hitChanceRNG = UnityEngine.Random.Range(0, 100); //Determines if the player got hit
                    //Debug.Log(hitChanceRNG);

                    if (hitChanceRNG <= hitChance)
                    {
                        playerStatsScript.TakeDamage(rangedDamage);
                        switch (starStoneID)
                        {
                            case 1: // Fire starstone
                                StartCoroutine(DamageOverTime(ticksOfDamageOverTime, damageOverTimeInterval, damageOverTime));                                
                                break;

                            case 2: //Ice starstone
                                StartCoroutine(PlayerSlow(slowPercentage, slowDuration));
                                break;

                            case 4: //Electric starstone
                                StartCoroutine(StunPlayer(stunDuration));
                                break;
                                 
                        }

                        StartCoroutine(SSEffectFeedback(starStoneID));
                        //Debug.Log("I shot you");
                    }
                    else
                    {
                        //Debug.Log("I missed");
                    }

                    isLoaded = false;
                    StartCoroutine(Reload()); //Starts a coroutine to reload the gun

                }
            }
            else
            {
                //Debug.Log("I can't see you");
                playerInSight = false;
            }
        }
    }

    IEnumerator DamageOverTime(int numberOfDamageTicks, float timeInterval, float damagePerTick) //Deals damage over time to the player
    {
        for(int i = 0; i < numberOfDamageTicks; i++)
        {
            yield return new WaitForSeconds(timeInterval);
            playerStatsScript.TakeDamage(damagePerTick);
        }
    }

    IEnumerator PlayerSlow(float xslowPercentage, float xslowDuration) //Slows player
    {
        playerMovementScript.speed *= xslowPercentage;
        yield return new WaitForSeconds(xslowDuration);
        playerMovementScript.speed = tempSpeed;
    }

    IEnumerator StunPlayer(float stunDuration) //Stuns the player (Only disables movement as disabling player shooting would make the game impossible to play)
    {
        waveHandlerScript.ToggleCharControler();
        yield return new WaitForSeconds(stunDuration);
        waveHandlerScript.ToggleCharControler();
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(rateOfFireCD);
        isLoaded = true;
        //Debug.Log("Reload coroutine completed");
    }

    IEnumerator SSEffectFeedback(int id) //Sets popup text, determines popup duration and enables the popup for the duriation of star stone effect (player feedback)
    {
        float duration = 0;
        switch (id)
        {
            case 1: // Fire starstone
                duration = damageOverTimeInterval * ticksOfDamageOverTime;
                ssEffectsText.text = "You're on fire";
                break;

            case 2: //Ice starstone
                duration = slowDuration;
                ssEffectsText.text = "You're slowed";
                break;

            case 4: //Electric starstone
                duration = stunDuration;
                ssEffectsText.text = "You're stunned";
                break;
        }
        ssEffectsPopup.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        ssEffectsPopup.gameObject.SetActive(false);
    }
}
