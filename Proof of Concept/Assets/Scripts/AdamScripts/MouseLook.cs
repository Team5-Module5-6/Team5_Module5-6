using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera movement from mouse input
public class MouseLook : MonoBehaviour
{
    // Sets mouse sensitivity in inspector
    public float mouseSensitivity = 100f;

    // Gets player transform to rotate horizontaly
    public Transform playerBody;

    // Camera vertical rotation value
    private float verticalRotation = 0f;

    void Start()
    {
        // Locks and hides cursor to game screen center so it doesn't click outside game tab
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Assigns mouse movement input to variables independt from frame rate to avoid delay
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Sets mouse vetical input into camera rotation and clamps at 90 degrees both up and down so player head doesn't spin
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90);

        // Rotates camera verticaly and player body horizontaly
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
