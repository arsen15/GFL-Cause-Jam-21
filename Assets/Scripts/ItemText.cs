using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemText : MonoBehaviour
{
    public TextMeshProUGUI itemText;
    int itemCount;
    public int itemNumberToUnlockDoor;

    public GameObject door;
    private bool doorDestroyed;

    private void Update()
    {
        if (itemCount == itemNumberToUnlockDoor && !doorDestroyed)
        {
            doorDestroyed = true;
            Destroy(door);
        }
        Debug.Log("test: "+gameObject.name);
    }

    private void OnEnable()
    {
        ItemManager.OnItemCollected += IncrementItemCount;
    }

    private void OnDisable()
    {
        ItemManager.OnItemCollected -= IncrementItemCount;
    }

    public void IncrementItemCount()
    {
        itemCount++;
        itemText.text = $" {itemCount}";
    }
}
