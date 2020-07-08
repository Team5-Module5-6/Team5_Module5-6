using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Moves player character around scene according to keyboard input
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform raycastTransform;

    public float speed = 12f;
    public float gravity = -19.6f;
    public float jumpHeight = 2f;
    public float ladderDistance = 1f;

    Vector3 velociy;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        RaycastHit raycastHit;
        if(Physics.Raycast(raycastTransform.position, raycastTransform.TransformDirection(Vector3.forward), out raycastHit))
        {
            if(raycastHit.transform.gameObject.CompareTag("Ladder") && raycastHit.distance < ladderDistance)
            {
                move = transform.up * z;
                gravity = 0f;
            }
            else
            {
                move = transform.right * x + transform.forward * z;
                gravity = -19.6f;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && controller.isGrounded)
        {
            controller.Move(move * speed * 1.5f * Time.deltaTime);
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.localScale = new Vector3(1f, 0.6f, 1f);
                controller.Move(move * speed * 0.5f * Time.deltaTime);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                controller.Move(move * speed * Time.deltaTime);
            }
        }

        velociy.y += gravity * Time.deltaTime;
        controller.Move(velociy * Time.deltaTime);

        if (controller.isGrounded && velociy.y < 0)
        {
            velociy.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velociy.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
