using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private float rotationSpeed = 5f;
    public GameObject playerReference;
    private PlayerMovement playerScript;
    public int ItemsNeeded;
    private bool locked = false;
    public GameObject doorLock;

    private void Start()
    {
        // Store the initial rotation (closed) and the target open rotation
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 90, 0));
        playerScript = playerReference.GetComponent<PlayerMovement>();
        if (doorLock != null)
        {
            locked = true;
        }
    }

    private void OnMouseDown()
    {
        if (ItemsNeeded >= playerScript.itemsFound && locked == true)
        {
            locked = false;
            Destroy(doorLock);

        }
        else if (locked == false)
        {
            if (CompareTag("door"))
            {
                isOpen = !isOpen;
                StopAllCoroutines();
                StartCoroutine(RotateDoor(isOpen ? openRotation : closedRotation));
            }
        }
        else
        {
            Debug.Log("It's Locked. Try to find objects to unlock it.");
        }

    }

    private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}