using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    [Header("Gravity Settings")]
    // [SerializeField] private Transform _playerTransform;
    // [SerializeField] private Rigidbody2D _rb;
    // [SerializeField] private float _influence;
    // [SerializeField] private float _intensity;
    // [SerializeField] private float _playerDistance;

    // private Vector2 _pullForce;
    private PlayerController _player;
    private Rigidbody2D _rb;

    private bool _top;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.gravityScale *= -1;
            Rotation();
        }

        // OnTriggerEnter2D(_player._collider);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if(other.CompareTag("Player"))
        {
            _playerDistance = Vector2.Distance(_playerRB.position, transform.position);
            if (_playerDistance <= _influence)
            {
                _pullForce = (transform.position - _playerTransform.position).normalized / _playerDistance * _intensity;
                _playerRB.AddForce(_pullForce, ForceMode2D.Force);
                _playerRB.gravityScale *= -10;
            }
        }*/

        if (other.CompareTag("Player"))
        {
            _rb.gravityScale *= -1;
            Rotation();
        }
    }

    private void Rotation()
    {
        if (_top == false)
            transform.eulerAngles = new Vector2(180f, 0);
        else
            transform.eulerAngles = Vector2.zero;

        _player.facingRight = !_player.facingRight;
        _top = !_top;
    }
}
