using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public Transform playerPosition;       // Assign this in the Inspector or find at runtime
    public float speed = 3f;       // Movement speed

    private Jumpscare jumpscareScript;
    public GameObject jumpscareTrigger;
    private Animator mAnimator;
    private bool playerCaught = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        jumpscareTrigger = GameObject.Find("StartChase");
        jumpscareScript = jumpscareTrigger.GetComponent<Jumpscare>();
        mAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (playerCaught == false)
        {
            playerPosition = player.transform;
            if (playerPosition == null) return;

            // Get target position on X and Z (ignore Y)
            Vector3 targetPosition = new Vector3(playerPosition.position.x, transform.position.y, playerPosition.position.z);

            // Move toward playerPosition
            if (jumpscareScript.animDone == true)
            {
                mAnimator.SetBool("IntroBool", false);

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                mAnimator.SetBool("IntroBool", true);
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCaught = true;
            mAnimator.SetTrigger("Caught");

            Camera playerCam = collision.gameObject.GetComponentInChildren<Camera>();

            if (playerCam != null)
            {
                CameraMovement camMove = playerCam.GetComponent<CameraMovement>();
                if (camMove != null) camMove.allowMouseControl = false;

                Vector3 dir = (transform.position - playerCam.transform.position).normalized;
                playerCam.transform.rotation = Quaternion.LookRotation(dir);
                StartCoroutine(death(2));
            }
        }
    }

    private IEnumerator death(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
