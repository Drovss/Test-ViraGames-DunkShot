using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] private Ball _ball;
    [SerializeField] private Trajectory _trajectory;
    [SerializeField] private float _pushForce = 4;

    private Camera _camera;
    private bool _isDragging = false;

    private Vector2 _startPoint;
    private Vector2 _endPoint;
    private Vector2 _direction;
    private Vector2 _force;
    private float _distance;

    private void Start()
    {
        _camera = Camera.main;
        _ball.deactivateRb();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            onDragStart();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            onDragEnd();
        }

        if (_isDragging)
        {
            onDrag();
        }
    }

    private void onDragStart()
    {
        _ball.deactivateRb();
        _startPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

        _trajectory.show();
    }
    private void onDrag()
    {
        _endPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        _distance = Vector2.Distance(_startPoint, _endPoint);
        _direction = (_startPoint - _endPoint).normalized;
        _force = _direction * (_distance * _pushForce);
        
        _trajectory.updateDots(_ball.Pos, _force);
        
        Debug.DrawLine(_startPoint, _endPoint);
    }
    private void onDragEnd()
    {
        _ball.activateRb();
        _ball.push(_force);
        
        _trajectory.hide();
    }
}