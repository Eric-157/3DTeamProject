using UnityEngine;
using System.Collections;

public class Jumpscare : MonoBehaviour
{
    public GameObject objectToSpawn;     // Assign in Inspector
    public float spawnDistanceBehind = 2f;
    public float lookDuration = 2f;
    public bool animDone = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform playerRoot = other.transform.parent;
            if (playerRoot == null) return;

            Vector3 spawnPosition = playerRoot.position - playerRoot.forward * spawnDistanceBehind;
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            animDone = false;

            Camera playerCamera = playerRoot.GetComponentInChildren<Camera>();
            if (playerCamera != null)
            {
                CameraMovement camMove = playerCamera.GetComponent<CameraMovement>();
                if (camMove != null)
                {
                    camMove.allowMouseControl = false;  // Disable mouse control
                }

                Vector3 direction = (spawnedObject.transform.position - playerCamera.transform.position).normalized;
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
        yield return new WaitForSeconds(delay);
        camMove.allowMouseControl = true;
        animDone = true;
    }
}