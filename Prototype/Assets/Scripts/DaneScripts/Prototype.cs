using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// All functions related to the basic prototype weapon functions
/// Fire laser on right click, reload when near generator, call SS power when firing
/// </summary>


public class Prototype : MonoBehaviour
{
    public LineRenderer laserBeam;
    public int maxAmmo = 100;
    public int currentAmmo;
    public int starStoneID;
    public float damage;
    public float chargeRate;//per second
    
    //script references    
    private EnemyStats target;
    private WaveHandler waveHandlerScript;

    // Start is called before the first frame update
    void Start()
    {
        laserBeam = GetComponent<LineRenderer>();

        waveHandlerScript = GameObject.FindObjectOfType<WaveHandler>();

        //Setting variables
        currentAmmo = maxAmmo;
        starStoneID = waveHandlerScript.starStoneID;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0)//Left mouse button and ammo is not empty
        {
            laserBeam.enabled = true;//Show the laser
            InvokeRepeating("LaserFire", 0f, 0.2f);//Adjust damage rate here(2nd float)
        }
        else if (Input.GetMouseButtonUp(0) || currentAmmo <= 0)
        {
            laserBeam.enabled = false; //Hide the laser
            CancelInvoke("LaserFire");//Stop firing
        }
    }

    void LaserFire()
    {       
        currentAmmo = currentAmmo - 1; //Depleat ammo when firing

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            if (hit.collider)//If something is hit by the laser
            {
                laserBeam.SetPosition(1, new Vector3(0, 0, hit.distance * 4)); //Position laser tip at the position of the hit object
            }
            target = hit.transform.GetComponent<EnemyStats>(); //Assign targets (enemy objects)
            if (target != null)//Only if hit object is an enemy
            {
                target.TakeDamage(damage); //Deal damage to the enemy
                StarStoneSelect(); //Carry out function based on current equipped star stone
            }
        }
        else
        {
            laserBeam.SetPosition(1, new Vector3(0, 0, 5000)); //Set laser distane when not hitting anything
        }
    }

    public void StarStoneSelect()
    {
        //Change colour of laser and activate SS power based on the current SS ID
        //All power functions located in "EnemyStats" script
        //This method makes the functions easier to manage and makes sure they affet the right enemies at the right time 
        switch (waveHandlerScript.starStoneID)
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

    void Charge()//Increase ammo
    {
        currentAmmo++;

        if (currentAmmo == maxAmmo)
        {
            CancelInvoke("Charge");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Generator" && currentAmmo < maxAmmo)
        {
            InvokeRepeating("Charge", 0.1f, chargeRate);//Recharge when in proximity of generator
        }
    }
}
