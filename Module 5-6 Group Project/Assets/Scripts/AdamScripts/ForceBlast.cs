using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForceBlast : MonoBehaviour
{
    public ParticleSystem blast;

    public float power = 10f;
    public float radius = 5f;
    public float upForce = 1f;
    public float cooldownTime = 10;
    public TextMeshProUGUI cooldownUI;
    
    private bool onCooldown = false;
    private float activeCooldown;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !onCooldown)
        {
            Yeet();
            blast.Play();
            onCooldown = true;
            activeCooldown = cooldownTime;
        }

        if(onCooldown && activeCooldown > 0)
        {
            cooldownUI.text = Mathf.Ceil(activeCooldown).ToString();
            activeCooldown -= 1 * Time.deltaTime;
        }
        else
        {
            onCooldown = false;
            cooldownUI.text = "Ready";
        }
    }

    void Yeet()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null && hit.gameObject.CompareTag("Enemy"))
            {
                rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
            }
        }
    }
}
