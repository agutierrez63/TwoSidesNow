using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    #region References
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 12f;
    [SerializeField] private float _jumpForce = 14f;
    [SerializeField] private int _maxJumps = 1;

    [Header("Gravity")]
    [SerializeField] public float _gravityScale = 4f;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private Transform _playerTransfrom;
    private float _thresholdY = -100;
    private Vector3 _initPosition;

    private Rigidbody2D _rb;
    private int _jumpCount;
    private bool _isFacingRight = true;
    private bool _isGravityReversed = false;


    private bool isPlayer1; // Flag to differentiate between Player 1 (WASD) and Player 2 (Arrow keys)
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = _gravityScale;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Determine which player this script instance belongs to based on tag or other identifier
        isPlayer1 = gameObject.CompareTag("Player");
    }

    private void Start()
    {
        _jumpCount = _maxJumps;
        _playerTransfrom = transform;
        _initPosition = _playerTransfrom.position;
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        ResetPlayerPosition();
    }

    void ResetPlayerPosition()
    {
        if (_playerTransfrom.position.y < _thresholdY)
        {
            _playerTransfrom.position = new Vector3(0, 0, 0);
        }
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

        Vector2 moveVelocity = new Vector2(moveInput * _moveSpeed, _rb.velocity.y);
        _rb.velocity = moveVelocity;

        // Flip sprite if moving in different direction
        if ((moveInput > 0 && !_isFacingRight) || (moveInput < 0 && _isFacingRight))
        {
            FlipHorizontal();
        }
    }

    private void HandleJump()
    {
        bool isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        if (isGrounded)
        {
            _jumpCount = _maxJumps;
        }

        // Perform jump action
        if ((!isPlayer1 && Input.GetButtonDown("Player2_Jump")) && _jumpCount > 0)
        {
            Jump();
        }

        if ((isPlayer1 && Input.GetButtonDown("Player1_Jump")) && _jumpCount > 0)
        {
            Jump();
        }
    }

    private void Jump()
    {
        float jumpDirection = _isGravityReversed ? -_jumpForce : _jumpForce;
        _rb.velocity = new Vector2(_rb.velocity.x, jumpDirection);
        _jumpCount--;
    }

    public void SetGravityReversed(bool reversed)
    {
        _isGravityReversed = reversed;
        //FlipVertical();
    }

    #region Vertical/Horizontal
    private void FlipHorizontal()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        _isFacingRight = !_isFacingRight;
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
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
    }
    #endregion
}
