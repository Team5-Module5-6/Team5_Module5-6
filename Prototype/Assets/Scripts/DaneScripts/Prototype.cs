using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prototype : MonoBehaviour
{
    public LineRenderer laserBeam;
    public float damage = 0.0001f;
    public int starStoneID = 0;

    public float timeOnFire = 5f;
    public bool onFire = false;

    //script reference
    private WaveHandler waveHandler;
    public EnemyStats enemyStats;
  
    // Start is called before the first frame update
    void Start()
    {
        laserBeam = GetComponent<LineRenderer>();
        waveHandler = GameObject.Find("WaveHandler").GetComponent<WaveHandler>();
        enemyStats = GameObject.FindObjectOfType<EnemyStats>();
        starStoneID = waveHandler.starStoneID;
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
              
    }
    public void LaserFire()
    {        
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            if (hit.collider)
            {
                laserBeam.SetPosition(1, new Vector3(0, 0, hit.distance * 4));

            }

            EnemyStats target = hit.transform.GetComponent<EnemyStats>();
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
        StarStoneSelect();

    }

    public void StarStoneSelect()
    {
        switch (starStoneID)
        {
            case 1:
                laserBeam.material.color = Color.red;
                StartCoroutine(FireStone());
                break;

            case 2:
                laserBeam.material.color = Color.cyan;
                StartCoroutine(IceStone());
                break;

            case 3:
                laserBeam.material.color = Color.green;
                StartCoroutine(PoisonStone());
                break;

            case 4:
                laserBeam.material.color = Color.yellow;
                StartCoroutine(ElectricityStone());
                break;
        }
    }

    public void FireEffect()
    {       
               
    }

    IEnumerator FireStone()
    {
        onFire = true;
        InvokeRepeating("FireEffect", 0f, 0.2f);
        Debug.Log("OnFire");

        yield return new WaitForSeconds(timeOnFire);

        onFire = false;
        CancelInvoke("FireEffect");

        Debug.Log("NotOnFire");
    } 

    public void IceEffect()
    {

    }

    IEnumerator IceStone()
    {
        InvokeRepeating("IceEffect", 0f, 0.2f);

        yield return new WaitForSeconds(timeOnFire);

        CancelInvoke("IceEffect");
    }

    public void PoisonEffect()
    {

    }

    IEnumerator PoisonStone()
    {
        InvokeRepeating("PoisonEffect", 0f, 0.2f);

        yield return new WaitForSeconds(timeOnFire);

        CancelInvoke("PoisonEffect");
    }

    public void ElectricityEffect()
    {

    }

    IEnumerator ElectricityStone()
    {
        InvokeRepeating("ElectricityEffect", 0f, 0.2f);

        yield return new WaitForSeconds(timeOnFire);

        CancelInvoke("ElectricityEffect");
    }
}
