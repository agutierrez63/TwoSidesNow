using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private PlayerMovement _player;
    private Rigidbody2D _rb;
    private bool _top = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerMovement>();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Particles"))
        {
            ToggleGravity();
            RotatePlayer();
        }
    }

    private void ToggleGravity()
    {
        _rb.gravityScale *= -1;
        _player.SetGravityReversed(_rb.gravityScale < 0);
    }

    private void RotatePlayer()
    {
        _player.FlipVertical();
        _top = !_top;
    }
}