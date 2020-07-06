using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//My trash movement script that wont be used nor submited
public class ScuffedPlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float rotationSpeed = 1;
    public Transform target, player;
    private float mouseX, mouseY;
    private SpawnerV2 spawnerScript;

    void Start()
    {

        spawnerScript = FindObjectOfType<SpawnerV2>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        CameraControll();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 playerMovement = new Vector3(x, 0, z) * speed * Time.deltaTime;
        player.Translate(playerMovement, Space.Self);

        if (Input.GetKeyDown("space")) { spawnerScript.SpawnerToggle(); }

    }

    void CameraControll()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -90, 90);

        transform.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
