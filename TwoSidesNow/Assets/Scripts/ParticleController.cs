using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ParticleController : MonoBehaviour
{
    #region Interface
    [Header("Particle Controller")]
    [SerializeField]
    private ParticleSystem _movementParticle;
    [SerializeField]
    private ParticleSystem _fallParticle;
    [Range(0, 10)]
    [SerializeField]
    private int _afterVelocity;
    [Range(0, 0.2f)]
    [SerializeField]
    private float _formationPeriod;
    [SerializeField]
    private Rigidbody2D _r2d;
    #endregion

    private float _counter;
    private bool _isGrounded;

    // Update is called once per frame
    void Update()
    {
        _counter += Time.deltaTime;
        if (_isGrounded && Mathf.Abs(_r2d.velocity.x) > _afterVelocity)
        {
            if (_counter > _formationPeriod)
            {
                _movementParticle.Play();
                _counter = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            _fallParticle.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
