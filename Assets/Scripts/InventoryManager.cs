using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Key is item type, Value is item name?
    private Dictionary<ItemType, int> inventory = new Dictionary<ItemType, int>();
    public Image[] itemImages;

    public Sprite waterSprite, snowballSprite, icicleSprite;
    public void AddItem(ItemType itemType, int quantity)
    {
        if (inventory.ContainsKey(itemType))
        {
            inventory[itemType] += quantity;
        } else
        {
            inventory.Add(itemType, quantity);
        }
        

        foreach(var item in inventory)
        {
            int itemIndex = (int)item.Key;
            itemImages[itemIndex].sprite = GetSpriteForItem(item.Key);
            itemImages[itemIndex].gameObject.SetActive(true);

            Debug.Log(item.Key + " : " + item.Value);
        }
    }

    private Sprite GetSpriteForItem(ItemType itemType)
    {
        if(itemType == ItemType.Water)
        {
            return waterSprite;
        } 
        else if (itemType == ItemType.Snowball)
        {
            return snowballSprite;
        }
        else if (itemType == ItemType.Icicle)
        {
            return icicleSprite;
        }
        return null;
    }
}
