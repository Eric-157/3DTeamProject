using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 25.0f;
    public Rigidbody rb;
    public GameObject jumpscareTrigger;
    public Transform playerCamera; // Assign your camera in the Inspector

    private Jumpscare jumpscareScript;
    public int itemsFound;

    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpscareScript = jumpscareTrigger.GetComponent<Jumpscare>();
    }

    void Update()
    {
        if (jumpscareScript.animDone == true)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            // Get camera forward and right, but zero out the Y to keep movement horizontal
            Vector3 camForward = playerCamera.forward;
            camForward.y = 0;
            camForward.Normalize();

            Vector3 camRight = playerCamera.right;
            camRight.y = 0;
            camRight.Normalize();

            // Combine inputs and directions
            Vector3 moveDirection = (camForward * verticalInput + camRight * horizontalInput).normalized;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                rb.velocity = moveDirection * speed + new Vector3(0, rb.velocity.y, 0);
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}