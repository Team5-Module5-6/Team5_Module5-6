using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prototype : MonoBehaviour
{
    public LineRenderer laserBeam;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        laserBeam = GetComponent<LineRenderer>();
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
            }
        }
        else
        {
            laserBeam.SetPosition(1, new Vector3(0, 0, 5000));
        }
    }
}
