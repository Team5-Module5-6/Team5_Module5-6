using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    public float timeOnFire = 9.9f;

    //Script References
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
        //enemy = other.GetComponent<EnemyStats>();
        //power = other.GetComponent<PlayerEffects>();
        //if (enemy != null)
        //{
        //    //StartCoroutine(FireEffect());
        //    if (power.onFire == true)
        //    {
        //        InvokeRepeating("FireDamage", 0.1f, 1f);
        //    }
        //    else if (power.onFire == false)
        //    {
        //        CancelInvoke("FireDamage");
        //        power.fireEffect.SetActive(false);
        //    }
        //}
     

    }

    //IEnumerator FireEffect()
    //{    
    //    Debug.Log("OnFire");
    //    protoScript.InvokeRepeating("FireDamage", 0.1f, 1f);

    //    yield return new WaitForSeconds(timeOnFire);

    //    Debug.Log("NotOnFire");
    //    protoScript.CancelInvoke("FireDamage");
             

        
    //}

    //public void FireDamage()
    //{
    //    if(power.onFire == true)
    //    {
    //        Debug.Log("YEET");
    //        //enemy.TakeDamage(power.fireDamage);
    //        protoScript.FireDamage();
    //    }
        
    //}
}
