using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpwardPullZone : MonoBehaviour
{
    [SerializeField] private float _pullForce = 10f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, _pullForce);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.gravityScale = 4;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.y, _pullForce);
        }
    }
}
