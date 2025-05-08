using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float fallMultiplier = 2.5f;
    public float jumpMultiplier = 2f;
    public float moveSpeed = 5f;
    public float dashForce = 20f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1f;
    public float jumpForce = 10f; 
    public Transform groundCheck;    
    public float groundCheckRadius = 0.2f; 
    public LayerMask groundLayer;
    public bool unlockedJumping = false;
    public bool unlockedDash = false;
    public bool unlockedDoubleJump = false;
    public bool unlockedWallJump = false;
    public float wallJumpForce = 10f;
    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallCheckRadius = 0.2f;  

    private bool usedDoubleJump = false;
    private bool canDashInAir = true;
    private bool isDashing;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool facingRight = true;
    private bool isGrounded;
    private bool isTouchingWall = false;
    private bool isWallJumping = false;
    //private float nextDashTime = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = 0;

        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
            if (!facingRight)
            {
                facingRight = true;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
            if (facingRight)
            {
                facingRight = false;
                Vector3 scale = transform.localScale;
                scale.x *= -1; 
                transform.localScale = scale;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && unlockedJumping && !isWallJumping)
        {
                Debug.Log("Skocio sam");
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && unlockedDoubleJump && !usedDoubleJump && !isTouchingWall) {
            Debug.Log("Dupli skok");
            usedDoubleJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && unlockedDash && !isDashing && (isGrounded || canDashInAir))
        {
            Debug.Log("Dasho sam");
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.Space) && isTouchingWall)
        {
            isWallJumping = true;
            WallJump();
        }

        if (isGrounded) {
            canDashInAir = true;
            usedDoubleJump = false;
            isWallJumping = false;
        }
    }

    void FixedUpdate()
    {

        if (isDashing) {
            return;
        }

        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckRadius, groundLayer) || Physics2D.OverlapCircle(wallCheckRight.position, wallCheckRadius, groundLayer);

        if (isGrounded)
        {
            isDashing = false;
            Debug.Log("Na podu sam");
        }
        else {
            Debug.Log("U zraku sam");
        }

        if (isTouchingWall)
        {
            Debug.Log("Dodirujem zid");
            rb.gravityScale = 0;
        }
        else
        {
            Debug.Log("Ne dodirujem zid");
            rb.gravityScale = 1;
        }

    }

    private IEnumerator Dash() {
        isDashing = true;

        Vector2 dashDirection = facingRight ? Vector2.right : Vector2.left;
        if (facingRight)
        {
            Debug.Log("Dash desno");
        }
        else {
            Debug.Log("Dash lijevo");
        }
        rb.velocity = new Vector2(dashDirection.x * dashForce, rb.velocity.y);
        
        if (!isGrounded){
            canDashInAir = false;
        }

        yield return new WaitForSeconds(dashDuration);

        //nextDashTime = Time.time + dashCooldown;
        isDashing = false;
    }

    private void WallJump()
    {
        if (isTouchingWall)
        {
            Debug.Log("Odskocio od zida");
            Vector2 wallJumpDirection = Vector2.zero;

            if (Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckRadius, groundLayer))
            {
                wallJumpDirection = Vector2.left;
            }
            else if (Physics2D.OverlapCircle(wallCheckRight.position, wallCheckRadius, groundLayer))
            {
                wallJumpDirection = Vector2.right;
            }

            rb.velocity = new Vector2(wallJumpDirection.x * wallJumpForce, jumpForce);

            canDashInAir = true;
            usedDoubleJump = false;

            if (wallJumpDirection == Vector2.right && !facingRight)
            {
                facingRight = true;
                Vector3 scale = transform.localScale;
                scale.x *= -1; 
                transform.localScale = scale;
            }
            else if (wallJumpDirection == Vector2.left && facingRight)
            {
                facingRight = false;
                Vector3 scale = transform.localScale;
                scale.x *= -1;  
                transform.localScale = scale;
            }
        }
    }
}
