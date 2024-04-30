using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private Rigidbody2D itemRB;
    public float dropForce = 5;

    //public string itemName;
    public ItemType itemType;
    private int quantity = 1;

    private InventoryManager inventoryManager;

    SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        itemRB = GetComponent<Rigidbody2D>();
        itemRB.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);

        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            soundManager.PlaySFX(soundManager.weaponPickup);
            inventoryManager.AddItem(itemType, quantity);
            Destroy(gameObject);
        }
        
    }
}

public enum ItemType
{
    Water,
    Snowball,
    Icicle,
    Empty,
};

