using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	private Camera mainCam;
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

	// Coyote Time
	private float coyoteTime = 0.2f;
	private float coyoteTimeCounter;

	private float jumpBufferTime = 0.2f;
	private float jumpBufferCounter;

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

	public AudioSource jump;

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
		if (!PauseMenu.GameIsPaused)
		{
			HandleMovement();
			HandleJumping();
			HandleShooting();
		}
		
	}

	private void FixedUpdate()
	{
		if (!PauseMenu.GameIsPaused)
		{
			HandleKnockback();
		}
	}

	private void HandleMovement()
	{
		if (!PauseMenu.GameIsPaused)
		{
			input = Input.GetAxisRaw("Horizontal");
			animator.SetFloat("Speed", Mathf.Abs(input));

			if (input < 0 && !facingRight || input > 0 && facingRight)
			{
				Flip();
			}
		}
	}

	private void HandleJumping()
	{
		isGrounded = Physics2D.OverlapCircle(feetPos.position, groundCheckCircle, groundLayer);

        if (isGrounded)
        {
			coyoteTimeCounter = coyoteTime;
        } else
		{
			coyoteTimeCounter -= Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
		{
			jumpBufferCounter = jumpBufferTime;
		} else
		{
			jumpBufferCounter -= Time.deltaTime;
		}

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
		{
            //playerRb.velocity = Vector2.up * jumpForce;
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            jump.Play();

			jumpBufferCounter = 0f;
			coyoteTimeCounter = 0f;
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
		Vector3 scale = transform.localScale;
       		scale.x *= -1; 
        	transform.localScale = scale;
	}

	private void HandleShooting()
	{
		if (!PauseMenu.GameIsPaused)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				animator.SetBool("isShooting", true);
				Shoot();
			}
			else if (Input.GetButtonUp("Fire1"))
			{
				animator.SetBool("isShooting", false);
			}
		}
	}

	private void Shoot()
	{
		GameObject bulletPrefab = inventoryManager.GetSelectedBulletPrefab();
    		if (bulletPrefab != null)
    		{
        		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        		Vector3 direction = (mousePos - bulletStart.position).normalized;
			if (direction.x > 0 && facingRight)
        		{
            			Flip(); 
        		}
        		else if (direction.x < 0 && !facingRight)
        		{
            			Flip(); 
        		}
       		 	float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

       		 	Instantiate(bulletPrefab, bulletStart.position, Quaternion.AngleAxis(rotZ, Vector3.forward));
    		}
		else
		{
			Debug.Log("No bullet prefab found for the selected ammo type.");
		}
	}

}
