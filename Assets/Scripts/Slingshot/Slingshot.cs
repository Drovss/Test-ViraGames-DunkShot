using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField] private LineRenderer[] _lineRenderers;
    [SerializeField] private Transform[] _stripPositions;
    [SerializeField] private Transform _center;
    [SerializeField] private Transform _idlePosition;
    [SerializeField] private float _maxLength;
    
    private bool _isMouseDown;
    private Vector3 _currentPosition;
    

    private void Start()
    {
        for (int i = 0; i < _lineRenderers.Length; i++)
        {
            _lineRenderers[i].positionCount = 2;
            _lineRenderers[i].SetPosition(0, _stripPositions[i].position);
        }
        
        //ball
        createBall();
    }

    private void Update()
    {
        if (_isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            _currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            _currentPosition = _center.position + Vector3.ClampMagnitude(_currentPosition-_center.position, _maxLength);
            setStrips(_currentPosition);
            
            //ball
            if (_ballCol)
            {
                _ballCol.enabled = true;
            }
        }
        else
        {
            resetStrips();
        }
    }

    private void OnMouseDown()
    {
        _isMouseDown = true;
    }

    private void OnMouseUp()
    {
        _isMouseDown = false;
        
        //ball
        shoot();
    }

    private void resetStrips()
    {
        _currentPosition = _idlePosition.position;
        setStrips(_currentPosition);
    }
    
    private void setStrips(Vector3 position)
    {
        for (int i = 0; i < _lineRenderers.Length; i++)
        {
            _lineRenderers[i].SetPosition(1, position);
        }

        //ball
        if (_ballRb)
        {
            var dir = position - _center.position;
            _ballRb.transform.position = position + dir.normalized * _ballPositionOffset;
            _ballRb.transform.right = -dir.normalized;
        }
    }
    
    
    
    

    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private float _ballPositionOffset;
    [SerializeField] private float _force;

    private Rigidbody2D _ballRb;
    private Collider2D _ballCol;

    private void createBall()
    {
        var ball = Instantiate(_ballPrefab);

        _ballRb = ball.GetComponent<Rigidbody2D>();
        _ballCol = ball.GetComponent<Collider2D>();

        _ballCol.enabled = false;
        _ballRb.isKinematic = true;

        resetStrips();
    }

    private void shoot()
    {
        _ballRb.isKinematic = false;
        Vector3 ballForce = (_currentPosition - _center.position) * _force * -1;
        _ballRb.velocity = ballForce;

        _ballRb = null;
        _ballCol = null;
        Invoke(nameof(createBall), 2);
    }
    
}
