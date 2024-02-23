using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Paramters")]
    [SerializeField]
    private float _maxSpeed = 8.4f;
    [SerializeField]
    private float _jumpHeight = 8.2f;
    [SerializeField]
    private float _gravityScale = 1.0f;

    #region Variables
    private Rigidbody2D _rb;
    private BoxCollider2D _mainCollider;
   
    private Vector3 groundCheckPos;

    private bool _isGrounded = false;
    private float _moveDirection = 0;

    public bool facingRight = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCollider = GetComponent<BoxCollider2D>();

        _rb.freezeRotation = true;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _rb.gravityScale = _gravityScale;

    }

    void Update()
    {
        Jump();
    }

    void FixedUpdate()
    {
        Movement();
    }

    #region CheckGrounded
    private void CheckGrounded()
    {
        Bounds colliderBounds = _mainCollider.bounds;
        float colliderRadius = _mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);

        if (_rb.gravityScale < 0)
        {
            groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 1.9f, 0);
        }
        else if(_rb.gravityScale > 0)
        {
            groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        }

        // Check if player is _isGrounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);

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
        // Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), _isGrounded ? Color.green : Color.red);
        // Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), _isGrounded ? Color.green : Color.red);
    }
    #endregion

    #region Movement
    private void Movement()
    {
        CheckGrounded();
        _moveDirection = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_moveDirection * _maxSpeed, _rb.velocity.y);
        
        if(facingRight == false && _moveDirection > 0)
        {
            Flip();
        } 
        
        else if(facingRight == true && _moveDirection < 0)
        {
            Flip();
        }
    }
    #endregion

    #region Flip
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    #endregion

    #region Jump
    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && _isGrounded)
        {
            if(_rb.gravityScale > 0)
            {
                _rb.velocity = Vector2.up * _jumpHeight;
            }
            
            else
            {
                _rb.velocity = Vector2.down * _jumpHeight;
            }
        }
    }
    #endregion

}
