using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private PlayerController _player;
    private Rigidbody2D _rb;

    private bool _top;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerController>();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Particles"))
        {
            _rb.gravityScale *= -1;
            Debug.Log("Gravity Scale: " + _rb.gravityScale);
            Rotation();
        }
    }

    private void Rotation()
    {
        if (_top == false)
            transform.eulerAngles = new Vector3(0, 0, 180f);
        else
            transform.eulerAngles = Vector3.zero;

        _player.facingRight = !_player.facingRight;
        _top = !_top;
    }
}