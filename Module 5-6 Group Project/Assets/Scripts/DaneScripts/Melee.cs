using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float meleeDamage = 1f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("meleeAnim", false);

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("meleeAnim", true);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyHealth target = other.GetComponent<EnemyHealth>();
        if (target != null)
        {
            target.TakeDamage(meleeDamage);
        }
    }
}
