using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script not used in final product
public class Power : MonoBehaviour
{
    public float timeOnFire = 9.9f;

    //Script References
    private EnemyStats enemy;
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
        if (enemy != null)
        {
            //StartCoroutine(FireEffect());
            if (enemy.onFire == true)
            {
                InvokeRepeating("FireDamage", 0.1f, 1f);
            }
            else if (enemy.onFire == false)
            {
                CancelInvoke("FireDamage");
                enemy.fireEffect.SetActive(false);
            }
        }


    }

    IEnumerator FireEffect()
    {
        Debug.Log("OnFire");
        InvokeRepeating("FireDamage", 0.1f, 1f);

        yield return new WaitForSeconds(timeOnFire);

        Debug.Log("NotOnFire");
        CancelInvoke("FireDamage");



    }

    public void FireDamage()
    {
        if (enemy.onFire == true)
        {
            Debug.Log("YEET");
            //enemy.TakeDamage(power.fireDamage);
            //enemy.FireDamage();
        }

    }
}
