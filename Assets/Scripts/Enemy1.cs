using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;

    // Make this a list in case we want more than 1 item
    public GameObject[] itemDrops;

    // Enemy movement
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    
    // Freeze Bullet interaction
    public float freezeTime = 3f;
    private bool isFrozen = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;  
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen)
        {
            return;
        }
        // Enemy Chasing logic 
        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(-5, 5, 5);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }

            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(5, 5, 5);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }
            // Enemy patrol logic
            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.5f)
                {
                    transform.localScale = new Vector3(-5, 5, 5);
                    patrolDestination = 1;
                }
            }

            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.5f)
                {
                    transform.localScale = new Vector3(5, 5, 5);
                    patrolDestination = 0;
                }
            }
        }

        
    }

    public void Freee()
    {
        if (!isFrozen)
        {
            StartCoroutine(FreezeCoroutine());
        }
    }

    private IEnumerator FreezeCoroutine()
    {
        isFrozen = true;

        float originalSpeed = moveSpeed;
        moveSpeed = 0;

        yield return new WaitForSeconds(freezeTime);

        moveSpeed = originalSpeed;
        isFrozen = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        DamagePopUpGenerator.current.CreatePopUp(transform.position, damage, Color.red);
        if ( currentHealth <= 0 )
        {
            Die();
            ItemDrop();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void ItemDrop()
    {
        for (int i = 0; i < itemDrops.Length; i++)
        {
            GameObject droppedBullet = Instantiate(itemDrops[i], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            PlayerBullet bulletScript = droppedBullet.GetComponent<PlayerBullet>();
            if (bulletScript != null)
            {
                bulletScript.InitializeBullet(false); // Ensure the bullet won't be destroyed on collision
            }
            Debug.Log("Item dropped");
        }
    }


}
