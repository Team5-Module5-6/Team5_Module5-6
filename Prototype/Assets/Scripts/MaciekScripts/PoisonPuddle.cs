//Author: Maciej Dowbor
//Module: MED5192 & MED5201
//Last Accessed: 25/06/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonPuddle : MonoBehaviour
{
    //SS effects feedback
    private Text ssEffectsText;
    private Image ssEffectsPopup;

    //Script variables
    private float poisonDamageOverTime;
    private float poisonDamageOverTimeInterval;
    private int poisonTicksOfDamageOverTime;
    private float poisonSlowPercentage;
    private float poisonSlowDuration;
    private float tempSpeed;
    private float duration;

    //Script references
    private EnemyStats enemyStatsScript;
    private PlayerStats playerStatsScript;
    private PlayerMovement playerMovementScript;
    private HoldSSEffectInfo ssPupupInforScript;



    void Start()
    {
        //Script references
        enemyStatsScript = FindObjectOfType<EnemyStats>();
        playerStatsScript = FindObjectOfType<PlayerStats>();
        playerMovementScript = FindObjectOfType<PlayerMovement>();
        ssPupupInforScript = FindObjectOfType<HoldSSEffectInfo>();

        //Script variables
        poisonDamageOverTime = enemyStatsScript.poisonDamageOverTime;
        poisonDamageOverTimeInterval = enemyStatsScript.poisonDamageOverTimeInterval;
        poisonTicksOfDamageOverTime = enemyStatsScript.poisonTicksOfDamageOverTime;
        poisonSlowPercentage = enemyStatsScript.poisonSlowPercentage;
        poisonSlowDuration = enemyStatsScript.poisonSlowDuration;
        tempSpeed = playerMovementScript.speed;

        //SS popup
        ssEffectsText = ssPupupInforScript.ssEffectsText;
        ssEffectsPopup = ssPupupInforScript.ssEffectsPopup;
        duration = poisonDamageOverTimeInterval * poisonTicksOfDamageOverTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(DamageOverTime(poisonTicksOfDamageOverTime, poisonDamageOverTimeInterval, poisonDamageOverTime));
            StartCoroutine(PlayerSlow(poisonSlowPercentage, poisonSlowDuration));
            StartCoroutine(SSEffectFeedback());
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

    IEnumerator SSEffectFeedback()
    {
        ssEffectsText.text = "You're poisoned";
        ssEffectsPopup.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        ssEffectsPopup.gameObject.SetActive(false);
    }
}
