using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 25.0f;
    public float horizontalInput;
    public float verticalInput;
    public Rigidbody rb;
    private Jumpscare jumpscareScript;
    public GameObject jumpscareTrigger;

    public int itemsFound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpscareScript = jumpscareTrigger.GetComponent<Jumpscare>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpscareScript.animDone == true)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            rb.MovePosition(transform.position + (transform.forward * verticalInput * speed * Time.deltaTime) + (transform.right * horizontalInput * speed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}