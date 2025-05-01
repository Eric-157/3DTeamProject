using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidyDoor : MonoBehaviour
{
    private bool isOpen = false;
    public GameObject playerReference;
    private PlayerMovement playerScript;
    public int ItemsNeeded;
    private bool locked = false;
    public GameObject doorLock;

    public float slideAmount = 3f; // How much the door should slide up
    public float slideSpeed = 2f;  // Speed of sliding

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private void Start()
    {
        playerScript = playerReference.GetComponent<PlayerMovement>();
        if (doorLock != null)
        {
            locked = true;
        }

        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * slideAmount;
    }

    private void OnMouseDown()
    {
        if (ItemsNeeded == playerScript.itemsFound && locked)
        {
            locked = false;
            Destroy(doorLock);
        }
        else if (locked == false)
        {
            if (CompareTag("door"))
            {
                isOpen = true;
                StopAllCoroutines();
                StartCoroutine(OpenDoor(isOpen));
            }
        }
        else
        {
            Debug.Log("It's Locked. Try to find objects to unlock it.");
        }
    }

    private IEnumerator OpenDoor(bool opening)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = opening ? openPosition : closedPosition;
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * slideSpeed;
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed);
            yield return null;
        }

        transform.position = targetPos;
    }
}