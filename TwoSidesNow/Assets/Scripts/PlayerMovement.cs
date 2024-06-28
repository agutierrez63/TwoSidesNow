using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region References
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 2;

    [Header("Gravity")]
    public float gravityScale = 3f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private int jumpCount;
    private bool isFacingRight = true;
    private bool isGravityReversed = false;

    private bool isPlayer1; // Flag to differentiate between Player 1 (WASD) and Player 2 (Arrow keys)
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Determine which player this script instance belongs to based on tag or other identifier
        isPlayer1 = gameObject.CompareTag("Player");
    }

    private void Start()
    {
        jumpCount = maxJumps;
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float moveInput = 0f;

        // Determine movement input based on player
        if (isPlayer1)
        {
            moveInput = Input.GetAxis("Player1_Horizontal");
        }
        else
        {
            moveInput = Input.GetAxis("Player2_Horizontal");
        }

        Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

        // Flip sprite if moving in different direction
        if ((moveInput > 0 && !isFacingRight) || (moveInput < 0 && isFacingRight))
        {
            FlipHorizontal();
        }
    }

    private void HandleJump()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            jumpCount = maxJumps;
        }

        // Perform jump action
        if ((isPlayer1 && Input.GetButtonDown("Player1_Jump")) || (!isPlayer1 && Input.GetButtonDown("Player2_Jump")) && jumpCount > 0)
        {
            float jumpDirection = isGravityReversed ? -jumpForce : jumpForce;
            rb.velocity = new Vector2(rb.velocity.x, jumpDirection);
            jumpCount--;
        }
    }

    public void SetGravityReversed(bool reversed)
    {
        isGravityReversed = reversed;
        //FlipVertical();
    }

    #region Vertical/Horizontal
    private void FlipHorizontal()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isFacingRight = !isFacingRight;
    }

    public void FlipVertical()
    {
        Vector3 localScale = transform.localScale;
        localScale.y *= -1;
        transform.localScale = localScale;
    }
    #endregion

    #region DEBUG
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    #endregion
}
