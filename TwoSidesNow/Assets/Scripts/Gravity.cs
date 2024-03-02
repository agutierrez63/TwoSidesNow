using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    #region Variables
    private PlayerController _player;
    private Rigidbody2D _rb;
    private float _angle;
    public bool top;
    #endregion

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
            Rotation();
        }
    }

    #region Rotation
    private void Rotation()
    {
        if (top == false)
            transform.eulerAngles = new Vector3(0, 0, 180f);
        else
            transform.eulerAngles = Vector3.zero;

        _player.isFacingRight = !_player.isFacingRight;
        top = !top;
    }
    #endregion
}