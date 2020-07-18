using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [Header("Fire StarStone Variables")]
    public GameObject fireEffect;
    public float timeOnFire;
    public bool onFire = false;

    [Header("Poison StarStone Variables")]
    public GameObject poisonEffect;
    public float timePoisoned;
    public bool poisoned = false;

    private Prototype protoScript;
    private EnemyStats enemyStatsScript;
    private Weapon weaponScript;
    
    // Start is called before the first frame update
    void Start()
    {
        protoScript = GameObject.FindObjectOfType<Prototype>();
        enemyStatsScript = GameObject.FindObjectOfType<EnemyStats>();
        weaponScript = GameObject.FindObjectOfType<Weapon>();
    }

    void Update()
    {
       
    }

    public void FireStone()
    {
        onFire = true;
        fireEffect.SetActive(true);
        StartCoroutine(FireEffect());

    }

    public IEnumerator FireEffect()
    {
        onFire = true;

        if (onFire == true)
        {
            fireEffect.SetActive(true);

            yield return new WaitForSeconds(timeOnFire);

            onFire = false;
            fireEffect.SetActive(false);
        }
    }

    public void IceStone()
    {

    }

    public void PoisonStone()
    {
        poisoned = true;
       
    }

    public IEnumerator PoisonEffect()
    {
        poisoned = true;
        weaponScript.damage = 10;

        if (poisoned == true)
        {
            poisonEffect.SetActive(true);

            yield return new WaitForSeconds(timePoisoned);

            poisoned = false;
            weaponScript.damage = 1;
            poisonEffect.SetActive(false);
        }

    }

    public void ElectricityStone()
    {

    }
}
