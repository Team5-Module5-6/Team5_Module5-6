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
                target.TakeDamage(damage);
                StarStoneSelect();
                
            }
        }
        else
        {
            laserBeam.SetPosition(1, new Vector3(0, 0, 5000));
        }
    }

    public void StarStoneSelect()
    {
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

    
   
}
