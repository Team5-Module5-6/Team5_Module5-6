using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealingStation : MonoBehaviour
{
    public GameObject playerAtachment;
    //public GameObject circle;
    public float playerHealth = 100f;
    public float healingPerSecond = 10f;
    public float radius = 5f;
    public float durationTime = 10f;
    public float cooldownTime = 60f;
    public TextMeshProUGUI cooldownUI;
    public TextMeshProUGUI healthUI;

    private Rigidbody rigidbody;
    private bool deployed = false;
    private bool onCooldown = false;
    private float activeCooldown;
    private float activeDuration;
    private float distance;

    void Start()
    {
        transform.parent = playerAtachment.transform;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        healthUI.text = "Health: " + Mathf.Round(playerHealth).ToString();
    }

    private void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, playerAtachment.transform.position);

        if (Input.GetKeyDown(KeyCode.Q) && !onCooldown)
        {
            Deploy();
            activeCooldown = cooldownTime;
            activeDuration = durationTime;
            onCooldown = true;
        }

        if (activeDuration > 0)
        {
            activeDuration -= 1 * Time.deltaTime;

            if (deployed && playerHealth < 100 && distance < radius)
            {
                playerHealth += healingPerSecond * Time.deltaTime;
                healthUI.text = "Health: " + Mathf.Round(playerHealth).ToString();
            }
        }

        if (onCooldown && activeCooldown > 0)
        {
            cooldownUI.text = Mathf.Ceil(activeCooldown).ToString();
            activeCooldown -= 1 * Time.deltaTime;
        }
        else
        {
            onCooldown = false;
            cooldownUI.text = "Ready";
            deployed = false;
            activeDuration = durationTime;
            transform.position = playerAtachment.transform.position;
            transform.parent = playerAtachment.transform;
            rigidbody.useGravity = false;
        }
    }

    void Deploy()
    {
        transform.parent = null;
        rigidbody.useGravity = true;
        deployed = true;
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
        //circle.SetActive(true);
    }
}
