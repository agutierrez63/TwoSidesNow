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
    [SerializeField]
    private float _moveDirection = 0;

    private bool _grounded = false;
    private Rigidbody2D _rb;
    private BoxCollider2D _mainCollider;

    public bool facingRight = true;

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
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);

        // Check if player is _grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);

        //Check if any of the overlapping colliders are not player collider, if so, set is_grounded to true
        _grounded = false;

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != _mainCollider)
                {
                    _grounded = true;
                    break;
                }
            }
        }
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
        // Jumping
        if ((Input.GetKeyDown(KeyCode.Space)) && _grounded)
        {
            //_rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight);
            if(_rb.gravityScale > 0)
            {
                _rb.velocity = Vector2.up * _jumpHeight;
            }
            
            else if(_rb.gravityScale < 0)
            {
                _rb.velocity = Vector2.down * _jumpHeight;
            }
        }
    }
    #endregion

}
