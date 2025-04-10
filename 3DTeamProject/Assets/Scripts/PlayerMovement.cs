using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 8.0f;
    public float horizontalInput;
    public float verticalInput;
    public Rigidbody rb;

    public int itemsFound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        rb.MovePosition(transform.position + (transform.forward * verticalInput * speed * Time.deltaTime) + (transform.right * horizontalInput * speed * Time.deltaTime));

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}