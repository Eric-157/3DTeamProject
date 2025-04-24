using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChaseTrigger : MonoBehaviour
{
    private Jumpscare jumpscareScript;
    public GameObject jumpscareTrigger;
    private GameObject enemy;
    public float lookDuration = 3f;
    public GameObject door;
    public Vector3 startPosition;
    public Vector3 endPosition;
    private float startTime;
    private float fractionOfJourney;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        startPosition = door.transform.position;
        endPosition = door.transform.position - new Vector3(0, 6, 0);
        jumpscareScript = jumpscareTrigger.GetComponent<Jumpscare>();
    }

    void Update()
    {
        float fractionOfJourney = ((Time.time - startTime)) / 6;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (other.CompareTag("Player"))
        {
            Transform playerRoot = other.transform.parent;
            if (playerRoot == null) return;

            Camera playerCamera = playerRoot.GetComponentInChildren<Camera>();
            jumpscareScript.animDone = false;

            if (playerCamera != null)
            {
                CameraMovement camMove = playerCamera.GetComponent<CameraMovement>();
                if (camMove != null)
                {
                    camMove.allowMouseControl = false;  // Disable mouse control
                }

                Vector3 direction = (enemy.transform.position - playerCamera.transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                playerCamera.transform.rotation = lookRotation;

                if (camMove != null)
                {
                    StartCoroutine(ReenableCameraControl(camMove, lookDuration));
                }
            }
        }
    }

    private IEnumerator ReenableCameraControl(CameraMovement camMove, float delay)
    {
        StartCoroutine(DoorClose(delay));
        yield return new WaitForSeconds(delay);
        camMove.allowMouseControl = true;
        jumpscareScript.animDone = true;
    }

    private IEnumerator DoorClose(float delay)
    {
        door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - 6, door.transform.position.z);
        //door.transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
        yield return new WaitForSeconds(delay);
    }
}
