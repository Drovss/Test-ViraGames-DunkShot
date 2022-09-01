using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CircleCollider2D _collider;

    public Vector3 Pos => transform.position;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
    }

    public void push(Vector2 force)
    {
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void activateRb()
    {
        _rb.isKinematic = false;
    }
    
    public void deactivateRb()
    {
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0f;
        _rb.isKinematic = true;
    }
}
