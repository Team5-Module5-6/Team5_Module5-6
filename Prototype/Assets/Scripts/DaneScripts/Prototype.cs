using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prototype : MonoBehaviour
{
    public LineRenderer laserBeam;
    public int maxAmmo = 100;
    public int currentAmmo;
    public int starStoneID = 1;
    public float damage = 0.001f;
    

    //script reference
    
    private EnemyStats target;
    private PlayerEffects power;

    // Start is called before the first frame update
    void Start()
    {
        laserBeam = GetComponent<LineRenderer>();
        //starStoneID = waveHandler.starStoneID;

        currentAmmo = maxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        laserBeam.enabled = false;

        if (Input.GetMouseButton(0))
        {
            laserBeam.enabled = true;
            LaserFire();
        }

        //if (Input.GetKeyDown(KeyCode.G)) 
        //{
        //    //Best way to change player's stone? Maybe Temporary
        //    ChangeStarStone1();
        //}
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
                target.TakeDamage(damage);
                StarStoneSelect();
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
        //StarStoneSelect();

    }

    public void StarStoneSelect()
    {
        switch (starStoneID)
        {
            case 1:
                laserBeam.material.color = Color.red;
                StartCoroutine(power.FireEffect());
                break;

            case 2:
                laserBeam.material.color = Color.cyan;
                //StartCoroutine(power.IceEffect());
                break;

            case 3:
                laserBeam.material.color = Color.green;
                StartCoroutine(power.PoisonEffect());
                break;

            case 4:
                laserBeam.material.color = Color.yellow;
                //StartCoroutine(power.ElectricityEffect());
                break;
        }
    }

}
