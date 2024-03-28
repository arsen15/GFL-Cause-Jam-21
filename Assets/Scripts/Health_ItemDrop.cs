using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_ItemDrop : MonoBehaviour
{
    private Rigidbody2D itemRB;
    public float dropForce = 5;
    public int healthAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        itemRB = GetComponent<Rigidbody2D>();
        itemRB.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Heal player and destroy object
            Debug.Log("Healing item");
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healthAmount);
            }
            
            Destroy(gameObject);
        }
        
    }
}