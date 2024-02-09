using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [Header("")]
    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody2D _rigidBody;

    [Header("Gravity Settings")]
    [SerializeField] private float _influence;
    [SerializeField] private float _intensity;
    [SerializeField] private float _playerDistance;
    [SerializeField] private Vector2 _pullForce;

    private bool _isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // TESTING PURPOSES ONLY!!!
        // TODO: Change to the the input system
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidBody.gravityScale *= -1;
            Rotation();
        }
        // _playerDistance = Vector2.Distance(_rigidBody.position, _transform.position);
        // if(_playerDistance <= )
    }

    void Rotation ()
    {
        if(_isGrounded)
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        } 
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        _isGrounded = !_isGrounded;
    }
}
