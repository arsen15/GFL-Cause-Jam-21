using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom : MonoBehaviour
{
    private GameObject player;
    //private Rigidbody2D rb;
    //public float bulletForce;
    public int enemyBulletDamage;

    public Snake snake;
    //private float timer;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        //Vector3 bulletDirection = player.transform.position - transform.position;
        //rb.velocity = new Vector2(bulletDirection.x, bulletDirection.y).normalized * bulletForce;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy bullet destroys when it is off screen
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPoint.x < 0 || screenPoint.x > Screen.width || screenPoint.y < 0 || screenPoint.y > Screen.height)
        {
            Destroy(gameObject);
        }
        if (snake == null && !gameObject.GetComponent<Rigidbody2D>()) gameObject.AddComponent<Rigidbody2D>();
        
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);

    }

        public void SetSnake(Snake snake) {
            this.snake = snake;
        }


    // Enemy bullet destroys when collides with player and player takes damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("LevelObject"))
        {
            Destroy(gameObject);
        }
        PlayerHealth playerHP = collision.GetComponent<PlayerHealth>();
        if (playerHP != null)
        {
            playerHP.TakeDamage(enemyBulletDamage);
        }
        
    }
}
