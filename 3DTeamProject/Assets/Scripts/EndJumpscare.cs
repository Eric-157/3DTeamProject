using UnityEngine;
using System.Collections;

public class EndJumpscare : MonoBehaviour
{
    public GameObject objectToSpawn;     // Assign in Inspector
    public float spawnDistanceBehind = 8f;
    public float lookDuration = 2f;
    public bool animDone = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform playerRoot = other.transform.parent;
            if (playerRoot == null) return;

            Vector3 flatRootForward = new Vector3(playerRoot.forward.x, 0f, playerRoot.forward.z).normalized;
            Vector3 spawnPosition = playerRoot.position - flatRootForward * spawnDistanceBehind;
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.Euler(0, 0, 0));
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
                    StartCoroutine(ReenableCameraControl(camMove, 1));
                }
            }
        }
    }

    private IEnumerator ReenableCameraControl(CameraMovement camMove, float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}