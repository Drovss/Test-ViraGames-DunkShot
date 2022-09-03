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
        _ball.DeactivateRb();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            OnDragStart();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            OnDragEnd();
        }

        if (_isDragging)
        {
            OnDrag();
        }
    }

    private void OnDragStart()
    {
        _ball.DeactivateRb();
        _startPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

        _trajectory.Show();
    }
    private void OnDrag()
    {
        _endPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        _distance = Vector2.Distance(_startPoint, _endPoint);
        _direction = (_startPoint - _endPoint).normalized;
        _force = _direction * (_distance * _pushForce);
        
        _trajectory.UpdateDots(_ball.Pos, _force);
        
        Debug.DrawLine(_startPoint, _endPoint);
    }
    private void OnDragEnd()
    {
        _ball.ActivateRb();
        _ball.Push(_force);
        
        _trajectory.Hide();
    }
}