using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    #region Interface
    [Header("Player Controller")]
    [SerializeField]
    private float _maxSpeed = 8.4f;
    [SerializeField]
    private float _jumpHeight = 12.8f;
    [SerializeField]
    private float _gravityScale = 3.0f;
    [SerializeField]
    private float _linearDrag = 0.0f;

    public bool isFacingRight = true;

    [Header("Particle Controller")]
    [SerializeField]
    private ParticleSystem _movementParticle;
    [Range(0, 10)]
    [SerializeField]
    private int _afterVelocity;
    [Range(0, 0.2f)]
    [SerializeField]
    private float _formationPeriod;
    #endregion

    #region References
    private Rigidbody2D _rb;
    private BoxCollider2D _mainCollider;
    private Vector3 _groundCheckPos;
    private float _counter;
    private float _moveDirection = 0;
    // private float _fallMultiplier = 2.5f;
    private bool _isGrounded = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _mainCollider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _rb.gravityScale = _gravityScale;
    }

    void Update()
    {
        // Jump();
        MovementParticles();
    }

    void FixedUpdate()
    {
        // Movement();
    }

    #region CheckGrounded
    private void CheckGrounded()
    {
        Bounds colliderBounds = _mainCollider.bounds;
        float colliderRadius = _mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);

        /*This is where the magic happens */
        if (_rb.gravityScale < 0)
        {
            _groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 1.9f, 0);
        }
        else if (_rb.gravityScale > 0)
        {
            _groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        }

        // Check if player is _isGrounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheckPos, colliderRadius);

        //Check if any of the overlapping colliders are not player collider, if so, set is_isGrounded to true
        _isGrounded = false;

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != _mainCollider)
                {
                    _isGrounded = true;
                    break;
                }
            }
        }

        // DEBUG: Used for checking if player is touching the ground
        Debug.DrawLine(_groundCheckPos, _groundCheckPos - new Vector3(0, colliderRadius, 0), _isGrounded ? Color.green : Color.red);
        Debug.DrawLine(_groundCheckPos, _groundCheckPos - new Vector3(colliderRadius, 0, 0), _isGrounded ? Color.green : Color.red);
    }
    #endregion

    #region Movement
    public void Movement(InputAction.CallbackContext context)
    {
        CheckGrounded();
        // _moveDirection = Input.GetAxis("Horizontal");
        _moveDirection = context.ReadValue<Vector2>().x;
        _rb.velocity = new Vector2(_moveDirection * _maxSpeed, _rb.velocity.y);
        
        if(isFacingRight && _moveDirection < 0f)
        {
            Flip();
        } 
        
        else if(!isFacingRight && _moveDirection > 0f)
        {
            Flip();
        }
    }
    #endregion

    #region Flip
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    #endregion

    #region Jump
    public void Jump(InputAction.CallbackContext context)
    {
        //if ((Input.GetKeyDown(KeyCode.Space)) && _isGrounded)
        if (context.performed && _isGrounded)
        {
            // This is checks positive (down) 
            if (_rb.gravityScale > 0)
            {
                _rb.velocity += Vector2.up * _jumpHeight;
            }

            // if the gravity is negative (up)
            else
            {
                _rb.velocity += Vector2.down * _jumpHeight;
            }

        }
    }
    #endregion

    #region MovementParticles
    private void MovementParticles()
    {
        _counter += Time.deltaTime;
        if (_isGrounded && Mathf.Abs(_rb.velocity.x) > _afterVelocity)
        {
            if (_counter > _formationPeriod)
            {
                _movementParticle.Play();
                _counter = 0;
            }
        }
    }
    #endregion
}
