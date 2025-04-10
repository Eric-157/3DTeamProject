using UnityEngine;

public class Collection : MonoBehaviour
{
    public GameObject playerReference;
    private PlayerMovement playerScript;

    void Start()
    {
        playerScript = playerReference.GetComponent<PlayerMovement>();
    }
    private void OnMouseDown()
    {
        if (CompareTag("item"))
        {
            playerScript.itemsFound++;
            Debug.Log("Items Found: " + playerScript.itemsFound);
            Destroy(gameObject);
        }
    }
}