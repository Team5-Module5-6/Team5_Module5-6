using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The health stats and functions of enemies
/// Script not used in proof of concept, see "EnemyStats" script
/// </summary>

public class EnemyHealth : MonoBehaviour
{
    public float health = 5f;

    public void TakeDamage(float damage)
    {
        //Depleat health by 1
        health -= damage;

        //Die when health runs out
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);//Enemy dies
    }
}
