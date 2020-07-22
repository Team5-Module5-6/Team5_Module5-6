using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
   
    
    public float damage = 1f;
    public float timeOnFire = 9.9f;

    private EnemyStats enemy;
    private PlayerEffects power;
    private Prototype protoScript;

    // Start is called before the first frame update
    void Start()
    {
        protoScript = GameObject.FindObjectOfType<Prototype>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        enemy = other.GetComponent<EnemyStats>();
        power = other.GetComponent<PlayerEffects>();
        if (enemy != null)
        {
            power.onFire = true;
            StartCoroutine(FireEffect());
        }

    }

    IEnumerator FireEffect()
    {
        if(power.onFire == true)
        {
            Debug.Log("OnFire");
            protoScript.InvokeRepeating("FireDamage", 0.1f, 1f);

            yield return new WaitForSeconds(timeOnFire);

            Debug.Log("NotOnFire");
            power.onFire = false;
            protoScript.CancelInvoke("FireDamage");
        }       

        
    }

    public void FireDamage()
    {
        Debug.Log("YEET");
        enemy.TakeDamage(damage);
    }
}
