using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;

    // Make this a list in case we want more than 1 item
    public GameObject[] itemDrops;

    // Enemy movement
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    public SpriteRenderer spriteRenderer;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    
    // Freeze Bullet interaction
    public float freezeTime = 3f;
    private bool isFrozen = false;

    public Animator animator;

    SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

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
        if (isChasing){
            if (isPlayerVerticallyDistant())
            {
                SetChasing(false);
            }

            if (transform.position.x > playerTransform.position.x)
            {
                spriteRenderer.flipX = (true);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                //soundManager.PlayMusic();
            }

            if (transform.position.x < playerTransform.position.x)
            {
                spriteRenderer.flipX = (false);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                //soundManager.PlayMusic();
            }
        } 
        else if (!isChasing) 
        {   
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                SetChasing(true);
            }

            // Enemy patrol logic
            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.5f)
                {
                    spriteRenderer.flipX = (false);
                    patrolDestination = 1;
                }
            }

            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.5f)
                {
                    spriteRenderer.flipX = (true);
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

    // Needs to be public because shooting the Boar makes it aggro.
    public void SetChasing(bool shouldChase) {
        isChasing = shouldChase;
        animator.SetBool("isChasing", shouldChase);
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
        soundManager.PlaySFX(soundManager.boarHurt);
        DamagePopUpGenerator.current.CreatePopUp(transform.position, damage, Color.red);
        if ( currentHealth <= 0 )
        {
            Die();
            ItemDrop();
            soundManager.PlaySFX(soundManager.boarHurt);
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

    private bool isPlayerVerticallyDistant() {
        return Math.Abs(Math.Abs(playerTransform.position.y) - Math.Abs(transform.position.y)) > 3;
    }


}
