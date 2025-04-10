using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float rotationY = 0f;
    private float sensitivity = 2f;
    public Transform player;
    public bool allowMouseControl = true;  // <- This flag controls camera input

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        if (allowMouseControl)
        {
            float inputY = Input.GetAxis("Mouse Y") * sensitivity;
            float inputX = Input.GetAxis("Mouse X") * sensitivity;

            rotationY -= inputY;
            rotationY = Mathf.Clamp(rotationY, -90f, 90f);
            transform.localEulerAngles = Vector3.right * rotationY;
            player.Rotate(Vector3.up * inputX);
        }
    }
}
