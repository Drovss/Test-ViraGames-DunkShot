using UnityEngine;

public class Ball : MonoBehaviour
{
    private Transform _tr;
    private Rigidbody2D _rb;
    private CircleCollider2D _col;

    private void Awake()
    {
        _tr = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CircleCollider2D>();
    }

    public void Push(Vector2 force)
    {
        ActivateRb();
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void ActivateRb()
    {
        _col.enabled = true;
        _rb.isKinematic = false;
    }
    
    public void DeactivateRb()
    {
        _col.enabled = false;
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0f;
        _rb.isKinematic = true;
    }

    public void SetPosition(Vector2 position)
    {
        _tr.position = position;
    }
}
