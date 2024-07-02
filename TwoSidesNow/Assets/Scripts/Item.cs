using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Particles"))
        {
            ToggleGravity(collider);
        }
    }

    private void ToggleGravity(Collider2D collider)
    {
        if (_rb != null)
        {
            _rb.gravityScale *= -1;
        }
        else
        {
            Debug.LogWarning("No Rigidbody2D found on object in affected layers.");
        }
    }
}
