﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public GameObject effectPosition;
    public float timeOnFire = 5f;
    public float damage = 1f;
    public bool onFire = false;

    private Prototype protoScript;
    private EnemyStats enemyHealthScript;
    
    // Start is called before the first frame update
    void Start()
    {
        protoScript = GameObject.FindObjectOfType<Prototype>();
        enemyHealthScript = GameObject.FindObjectOfType<EnemyStats>();
    }

    void Update()
    {
       
    }

    public void FireStone()
    {
        onFire = true;
        effectPosition.SetActive(true);
        StartCoroutine(FireEffect());

    }

    IEnumerator FireEffect()
    {
        
        if (onFire == true)
        {
            effectPosition.SetActive(true);
            
            

            yield return new WaitForSeconds(timeOnFire);

            onFire = false;
            effectPosition.SetActive(false);
            
            
        }
    }

    public void IceStone()
    {

    }

    public void PoisonStone()
    {

    }

    public void ElectricityStone()
    {

    }
}
