using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    // PARAMETERS: Player
    private float _maxSpeed = 8.4f;
    private float _jumpHeight = 12.5f;
    private float _gravityScale = 1.0f;

    private Camera _mainCamera;
    private Vector3 _cameraPos;

    public bool facingRight = true;
    public float moveDirection = 0;
    public bool grounded = false;

    public BoxCollider2D _collider;
    public Transform _t;

    private Rigidbody2D _rb;
    private CapsuleCollider2D _capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        _t = transform;
        _rb = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();

        _rb.freezeRotation = true;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _rb.gravityScale = _gravityScale;

        facingRight = _t.localScale.x > 0;

        if (_mainCamera)
        {
            _cameraPos = _mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (grounded || Mathf.Abs(_rb.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (grounded || _rb.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                _t.localScale = new Vector3(Mathf.Abs(_t.localScale.x), _t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                _t.localScale = new Vector3(-Mathf.Abs(_t.localScale.x), _t.localScale.y, _t.localScale.z);
            }
        }

        // Jumping
        if ((Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.Space))) && grounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight);
        }

        // Camera follow
        if (_mainCamera)
        {
            _mainCamera.transform.position = new Vector3(_t.position.x, _cameraPos.y, _cameraPos.z);
        }
    }


    void FixedUpdate()
    {
        Bounds colliderBounds = _capsuleCollider.bounds;
        float colliderRadius = _capsuleCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);

        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        grounded = false;

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != _capsuleCollider)
                {
                    grounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        _rb.velocity = new Vector2((moveDirection) * _maxSpeed, _rb.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), grounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), grounded ? Color.green : Color.red);
    }
}
