using UnityEngine;

public class Collection : MonoBehaviour
{
    public static int itemsFound = 0;

    private void OnMouseDown()
    {
        if (CompareTag("item"))
        {
            itemsFound++;
            Debug.Log("Items Found: " + itemsFound);
            Destroy(gameObject);
        }
    }
}