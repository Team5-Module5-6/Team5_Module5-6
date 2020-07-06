using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public int meleeDamage = 1;

    
    public Animator animator;
    public WeaponSwitch weaponSwitch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //play idle melee weapon animation
        animator.SetBool("meleeAnim", false);
       
    } 

    void OnTriggerEnter(Collider other)
    {
        EnemyStats target = other.GetComponent<EnemyStats>();
        if (target != null)
        {
            target.TakeDamage(meleeDamage);
        }
    }
}
