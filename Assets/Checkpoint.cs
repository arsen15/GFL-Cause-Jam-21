using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    PlayerHealth playerHealth;
    public Transform respawnPoint;

    SpriteRenderer spriteRenderer;
    public Sprite passive, active;

    Collider2D coll;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth.UpdateCheckpoint(respawnPoint.position);
            spriteRenderer.sprite = active;
            coll.enabled = false;
        }

    }
}
