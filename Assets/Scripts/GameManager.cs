using UnityEngine;
using UnityEngine.Events;

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
    
    [SerializeField] private float _pushForce;
    [SerializeField] private float _minForce;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _minDistance;

    [HideInInspector] public UnityEvent  StartMoveEvent;
    [HideInInspector] public UnityEvent <Vector2> PushEvent;
    [HideInInspector] public UnityEvent <Vector2> UpdateTrajectoryEvent;
    
    private Camera _camera;
    private bool _isDragging;

    private Vector2 _startPoint;
    private Vector2 _endPoint;
    private Vector2 _direction;
    private Vector2 _force;
    private float _distance;
    
    public Ball Ball { get; set; }

    private void Start()
    {
        _camera = Camera.main;
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
        StartMoveEvent?.Invoke();
        _startPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnDrag()
    {
        _endPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        _distance = Vector2.Distance(_startPoint, _endPoint);
        _direction = (_startPoint - _endPoint).normalized;
        _force = _direction * (_distance * _pushForce);

        UpdateTrajectoryEvent?.Invoke(NormalizeForce(_force));
        
        Debug.DrawLine(_startPoint, _endPoint);
    }
    private void OnDragEnd()
    {
        if (_distance < _minDistance) return;
        
        PushEvent?.Invoke(NormalizeForce(_force));
    }

    private Vector2 NormalizeForce(Vector2 force)
    {
        if (force.magnitude > _maxForce)
        {
            force = force.normalized * _maxForce;
        }
        else if (force.magnitude < _minForce)
        {
            force = force.normalized;
        }

        return force;
    }

    public void UpdatePosition(Transform tr)
    {
        var y = Instance.Ball.transform.position.y;
        if (y > tr.position.y)
        {
            var position = tr.position;
            position = new Vector3(position.x, y, position.z);
            tr.position = position;
        }
    }
    
    
}

