using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    // Movement & Jumping
    public Rigidbody2D playerRb;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;
    public float jumpForce;
    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPos;
    public float groundCheckCircle;
    private bool facingRight;

    // Knockback
    public float knockBackForce;
    public float knockBackCounter;
    public float knockBackTotalTime;
    public bool knockFromRight;

    // Animation
    public Animator animator;

    // Shooting
    public Transform bulletStart;
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("No inventory manager found!");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleShooting();
    }

    private void FixedUpdate()
    {
        HandleKnockback();
    }

    private void HandleMovement()
    {
        input = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(input));

        if (input < 0 && !facingRight || input > 0 && facingRight)
        {
            Flip();
        }
    }

    private void HandleJumping()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundCheckCircle, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.velocity = Vector2.up * jumpForce;
        }
    }

    private void HandleKnockback()
    {
        if (knockBackCounter <= 0)
        {
            playerRb.velocity = new Vector2(input * speed, playerRb.velocity.y);
        }
        else
        {
            var direction = knockFromRight ? -1 : 1;
            playerRb.velocity = new Vector2(direction * knockBackForce, knockBackForce);
            knockBackCounter -= Time.deltaTime;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void HandleShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("isShooting", true);
            Shoot();
        } else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("isShooting", false);
        }
    }

    private void Shoot()
    {
        GameObject bulletPrefab = inventoryManager.GetSelectedBulletPrefab();
        if (bulletPrefab != null)
        {
            Instantiate(bulletPrefab, bulletStart.position, bulletStart.rotation);
        }
        else
        {
            Debug.Log("No bullet prefab found for the selected ammo type.");
        }
    }
}
