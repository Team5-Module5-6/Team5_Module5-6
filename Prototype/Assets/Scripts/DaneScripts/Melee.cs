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
        animator.SetBool("meleeAnim", false);//play idle melee weapon animation

    } 

    void OnTriggerEnter(Collider other)
    {
        EnemyStats target = other.GetComponent<EnemyStats>();
        if (target != null)//Only if weapon hits an enemy
        {
            target.TakeDamage(meleeDamage);//Deal selected amount of damage to the enemy
        }
    }
}
