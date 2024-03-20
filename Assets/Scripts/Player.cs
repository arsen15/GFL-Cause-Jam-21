using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    public float knockBackForce;
    public float knockBackCounter;
    public float knockBackTotalTime;

    public bool knockFromRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");

        if (input < 0 && !facingRight)
        {
            Flip();
        } else if (input > 0 && facingRight)
        {
            //spriteRenderer.flipX = false;
            Flip();
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundCheckCircle, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.velocity = Vector2.up * jumpForce;
        }

        
    }

    private void FixedUpdate()
    {
        if (knockBackCounter <= 0)
        {
            playerRb.velocity = new Vector2(input * speed, playerRb.velocity.y);
        }
        else
        {
            if (knockFromRight == true)
            {
                playerRb.velocity = new Vector2(-knockBackForce, knockBackForce);
            }

            if (knockFromRight == false)
            {
                playerRb.velocity = new Vector2(knockBackForce, knockBackForce);
            }

            knockBackCounter -= Time.deltaTime;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    
}
