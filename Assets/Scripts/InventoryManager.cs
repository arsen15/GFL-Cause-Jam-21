using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    // Key is item type, Value is item quantity
    private Dictionary<ItemType, int> inventory = new Dictionary<ItemType, int>();
    public Image[] itemImages;

    public Sprite waterSprite, snowballSprite, icicleSprite;

    // Array to keep track of which slots the items are going to
    public ItemType[] currentItem;

    public ItemType SelectedAmmoType { get; private set; } = ItemType.Empty;

    public GameObject waterPrefab;
    public GameObject snowballPrefab;
    public GameObject iciclePrefab;

    private void Start()
    {
        AddItem(ItemType.Water, 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnButtonClicked(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnButtonClicked(1); 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnButtonClicked(2); 
        }
    }


    public void OnButtonClicked(int slotNumber)
    {
        ItemType itemToUse = currentItem[slotNumber];
        SelectedAmmoType = itemToUse;

        //Update Dictionary
        inventory[itemToUse] -= 1;

        // Update UI
        UpdateUI(itemToUse, inventory[itemToUse]);

        //Use the item
        if (itemToUse == ItemType.Water)
        {
            useWater();
        }
        else if (itemToUse == ItemType.Snowball)
        {
            useSnowball();
        }
        else if (itemToUse == ItemType.Icicle)
        {
            useIcicle();
        }
    }

    public GameObject GetSelectedBulletPrefab()
    {
        switch (SelectedAmmoType)
        {
            case ItemType.Water:
                return waterPrefab;
            case ItemType.Snowball:
                return snowballPrefab;
            case ItemType.Icicle:
                return iciclePrefab;
            default:
                return null;
        }
    }

    #region Add Item
    public void AddItem(ItemType itemType, int quantity)
    {
        if (inventory.ContainsKey(itemType))
        {
            inventory[itemType] = quantity;
        }
        else
        {
            inventory.Add(itemType, quantity);
        }

        UpdateUI(itemType, inventory[itemType]);


    }

    private void UpdateUI(ItemType itemType, int quantity)
    {
        // Check if we already have the item
        for (int i = 0; i < currentItem.Length; i++)
        {
            if (currentItem[i] == itemType)
            {
                
                return;
            }
        }

        // Check for the first empty slot
        for (int i = 0; i < currentItem.Length; i++)
        {
            if (currentItem[i] == ItemType.Empty)
            {
                itemImages[i].sprite = GetSpriteForItem(itemType);
                currentItem[i] = itemType;
                return;
            }
        }
    }

    private Sprite GetSpriteForItem(ItemType itemType)
    {
        if (itemType == ItemType.Water)
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
    #endregion

    #region Use Item
    private void useWater()
    {
        Debug.Log("Clicked on water slot");
    }

    private void useSnowball()
    {
        Debug.Log("Clicked on snowball slot");
    }

    private void useIcicle()
    {
        Debug.Log("Clicked on icicle slot");
    }

    #endregion
}

