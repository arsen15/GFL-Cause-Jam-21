using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                // Update this instance with data from the persistent instance
                CopyInventoryFromInstance(instance);
                Destroy(instance.gameObject); // Destroy the old instance
                instance = this; // Set this as the new instance
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    void CopyInventoryFromInstance(InventoryManager existingInstance)
    {
        // Copy inventory data from the existing instance to this one
        inventory = new Dictionary<ItemType, int>(existingInstance.inventory);
        currentItem = (ItemType[])existingInstance.currentItem.Clone();
        SelectedAmmoType = existingInstance.SelectedAmmoType;

        // Debug to ensure data is copied
        foreach (var item in inventory)
        {
            Debug.Log($"Item: {item.Key}, Quantity: {item.Value}");
        }

        // Make sure to also update UI elements to reflect the new state
        for (int i = 0; i < currentItem.Length; i++)
        {
            if (currentItem[i] != ItemType.Empty && inventory.ContainsKey(currentItem[i]))
            {
                itemImages[i].sprite = GetSpriteForItem(currentItem[i]);
                // Update any additional UI elements or item counts as needed
            }
            else
            {
                // Clear or hide UI elements for empty slots
                itemImages[i].sprite = null;
            }
        }

    }
    private void Start()
    {
        if (inventory.Count == 0)
        {
            // Initial inventory setup
            AddItem(ItemType.Water, 1);
            // Ensure the first slot is selected by default only if it's not empty
            if (currentItem.Length > 0 && currentItem[0] != ItemType.Empty)
            {
                OnButtonClicked(0);
            }
        }
        else
        {
            // The inventory has been copied, so we update the UI to reflect the current state
            UpdateAllUI();
        }
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

    void UpdateAllUI()
    {
        for (int i = 0; i < currentItem.Length; i++)
        {
            if (currentItem[i] != ItemType.Empty && inventory.ContainsKey(currentItem[i]))
            {
                itemImages[i].sprite = GetSpriteForItem(currentItem[i]);
                // Update any additional UI elements to reflect item counts or other relevant info
            }
            else
            {
                // Clear or hide UI elements for empty slots
                itemImages[i].sprite = null;
            }
        }
    }

    public void OnButtonClicked(int slotNumber)
    {
        if (slotNumber < 0 || slotNumber >= currentItem.Length)
            return; // Guard clause to prevent out-of-bounds access

        ItemType itemToUse = currentItem[slotNumber];

        if (itemToUse == ItemType.Empty || !inventory.ContainsKey(itemToUse) || inventory[itemToUse] <= 0)
        {
            Debug.Log("Slot is empty or item count is zero.");
            return; // Prevent using an item if the slot is empty or count is zero
        }

        SelectedAmmoType = itemToUse;

        // Decrement the item count in the inventory
        //inventory[itemToUse] -= 1;

        // Update UI
        UpdateUI(itemToUse, inventory[itemToUse]);

        // Use the item
        switch (itemToUse)
        {
            case ItemType.Water:
                useWater();
                break;
            case ItemType.Snowball:
                useSnowball();
                break;
            case ItemType.Icicle:
                useIcicle();
                break;
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

