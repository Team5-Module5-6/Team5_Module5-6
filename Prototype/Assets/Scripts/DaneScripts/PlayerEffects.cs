﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    //[Header("Fire StarStone Variables")]
    //public GameObject fireEffect;
    //public float timeOnFire;
    //public float fireDamage = 0.01f;
  

    //[Header("Ice StarStone Variables")]
    //public GameObject iceEffect;
    //public float timeFrozen;
    //public float defaultSpeed;//must equal normal speed for this enemy type
    //public float frozenSpeed;//speed when under effect of ice star stone
    //public bool frozen = false;

    //[Header("Poison StarStone Variables")]
    //public GameObject poisonEffect;
    //public float timePoisoned;
    //public bool poisoned = false;
    
    //[Header("Poison StarStone Variables")]
    //public GameObject electricEffect;
    //public float timeStunned = 10;
    //public bool stunned = false;

    ////Script References
    //private Prototype protoScript;
    //private EnemyStats enemyStatsScript;
    //private Weapon weaponScript;
    //private EnemyMovement enemyMovementScript;
    
    //// Start is called before the first frame update
    //void Start()
    //{
    //    protoScript = GameObject.FindObjectOfType<Prototype>();
    //    enemyStatsScript = GameObject.FindObjectOfType<EnemyStats>();
    //    weaponScript = GameObject.FindObjectOfType<Weapon>();
    //    enemyMovementScript = GameObject.FindObjectOfType<EnemyMovement>();
        
    //    frozenSpeed = defaultSpeed / 2;
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Fire")
    //    {
    //        enemyStatsScript.TakeDamage(fireDamage);
    //    }
    //}

    //public IEnumerator FireEffect()
    //{
    //    enemyStatsScript.onFire = true;

    //    if (enemyStatsScript.onFire == true)
    //    {
    //        fireEffect.SetActive(true);

    //        yield return new WaitForSeconds(timeOnFire);

    //        enemyStatsScript.onFire = false;
    //        fireEffect.SetActive(false);
    //    }
    //}

    //public IEnumerator IceEffect()
    //{
    //    frozen = true;
    //    iceEffect.SetActive(true);
    //    protoScript.SlowEnemy();

    //    yield return new WaitForSeconds(timeFrozen);

    //    frozen = false;
    //    iceEffect.SetActive(false);
    //    protoScript.SlowEnemy();
    //}

    //public IEnumerator PoisonEffect()
    //{
    //    poisoned = true;
    //    weaponScript.damage = 10;

    //    if (poisoned == true)
    //    {
    //        poisonEffect.SetActive(true);

    //        yield return new WaitForSeconds(timePoisoned);

    //        poisoned = false;
    //        poisonEffect.SetActive(false);
    //        weaponScript.damage = 1;
           
    //    }

    //}

    //public IEnumerator ElectricityEffect()
    //{
    //    stunned = true;
    //    electricEffect.SetActive(true);
    //    protoScript.StunEnemy();

    //    yield return new WaitForSeconds(timeStunned);

    //    stunned = false;
    //    electricEffect.SetActive(false);
    //    protoScript.StunEnemy();
    //}
}
