using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prototype : MonoBehaviour
{
    public LineRenderer laserBeam;
    public int maxAmmo = 1000;
    public int currentAmmo;
    public int starStoneID;
    public float damage = 0.001f;
    private string sceneName; //Maciek
    
    //script reference
    
    private EnemyStats target;
    private PlayerEffects power;
    private WaveHandler waveHandlerScript;

    // Start is called before the first frame update
    void Start()
    {
        laserBeam = GetComponent<LineRenderer>();

        waveHandlerScript = GameObject.FindObjectOfType<WaveHandler>();

        currentAmmo = maxAmmo;

        starStoneID = waveHandlerScript.starStoneID;

        sceneName = SceneManager.GetActiveScene().name; //Maciek
    }
    // Update is called once per frame
    void Update()
    {
        laserBeam.enabled = false;

        if (Input.GetMouseButton(0) && currentAmmo > 0)
        {
            laserBeam.enabled = true;
            LaserFire();
        }
    }

    public void LaserFire()
    {
        currentAmmo = currentAmmo - 1;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            if (hit.collider)
            {
                laserBeam.SetPosition(1, new Vector3(0, 0, hit.distance * 4));

            }

            target = hit.transform.GetComponent<EnemyStats>();
            power = hit.transform.GetComponent<PlayerEffects>();
            if (target != null)
            {
                //target.TakeDamage(damage);
                StarStoneSelect();
                //StartCoroutine(power.ElectricityEffect());
                //StartCoroutine(power.PoisonEffect());
                //StartCoroutine(power.IceEffect());
            }
        }
        else
        {
            laserBeam.SetPosition(1, new Vector3(0, 0, 5000));
        }
    }

    public void ChangeStarStone1()
    {
        if (starStoneID >= 0 && starStoneID <= 3)
        {
            starStoneID++;
        }
        else
        {
            starStoneID = 1;
        }


    }

    public void StarStoneSelect()
    {
        switch (starStoneID)
        {
            case 1:
                laserBeam.material.color = Color.red;
                StartCoroutine(target.FireEffect());
                break;

            case 2:
                laserBeam.material.color = Color.cyan;
                StartCoroutine(target.IceEffect());

                break;

            case 3:
                laserBeam.material.color = Color.green;
                StartCoroutine(target.PoisonEffect());
                break;

            case 4:
                laserBeam.material.color = Color.yellow;
                StartCoroutine(target.ElectricityEffect());
                break;
        }

    }

    //Star stone power functions
    //These functions are called via a script on the enemies
    //public void FireDamage()
    //{

    //    Debug.Log("YEET");
    //    target.TakeDamage(power.fireDamage);
    //}

    //public void SlowEnemy()
    //{
    //    if(power.frozen == true)
    //    {
    //        //Decrease enemy speed by half
    //        target.speed = power.frozenSpeed;
    //    }
    //    else if(power.frozen == false)
    //    {
    //        target.speed = power.defaultSpeed;
    //    }
    //}

    //public void StunEnemy()
    //{
    //    if (power.stunned == true)
    //    {
    //        target.GetComponent<EnemyMovement>().enabled = false;
    //    }
    //    else if (power.stunned == false)
    //    {
    //        target.GetComponent<EnemyMovement>().enabled = true;
    //    }
    //}

}
