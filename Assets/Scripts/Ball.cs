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

    public void Push(Vector2 force)
    {
        ActivateRb();
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void ActivateRb()
    {
        _rb.isKinematic = false;
    }
    
    public void DeactivateRb()
    {
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0f;
        _rb.isKinematic = true;
    }
}
