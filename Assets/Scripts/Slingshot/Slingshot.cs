using UnityEngine;

public class Slingshot : MonoBehaviour
{
    //[SerializeField] private LineRenderer[] _lineRenderers;
    //[SerializeField] private Transform[] _stripPositions;
    [SerializeField] private Transform _center;
    [SerializeField] private Transform _idlePosition;
    [SerializeField] private float _maxLength;
    [SerializeField] private Trajectory _trajectory;
    [SerializeField] private Transform _basket;
    
    
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private float _ballPositionOffset;
    [SerializeField] private float _force;

    private Ball _ball;
    //private Rigidbody2D _ballRb;
    //private Collider2D _ballCol;
    
    private bool _isMouseDown;
    private Vector3 _currentPosition;
    
    private Camera _camera;
    
    // private Vector2 _startPoint;
    // private Vector2 _endPoint;
    // private Vector2 _direction;
    // private Vector2 _force2;
    // private float _distance;
    //
    private void Start()
    {
        _camera = Camera.main;
        
        // for (int i = 0; i < _lineRenderers.Length; i++)
        // {
        //     _lineRenderers[i].positionCount = 2;
        //     _lineRenderers[i].SetPosition(0, _stripPositions[i].position);
        // }
        //
        //ball
        CreateBall();
    }

    private void Update()
    {
        if (_isMouseDown)
        {
            SetBall();

            Vector3 ballForce = (_currentPosition - _center.position) * (_force * -1);
            _trajectory.UpdateDots(_ball.transform.position, ballForce);

            RotateBasket();
            
            //ball
            // if (_ballCol)
            // {
            //     _ballCol.enabled = true;
            // }
        }
        else
        {
            //ResetStrips();
        }
    }

    private void SetBall()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        _currentPosition = _camera.ScreenToWorldPoint(mousePosition);
        var position = _center.position;
        _currentPosition = position + Vector3.ClampMagnitude(_currentPosition - position, _maxLength);
        SetStrips(_currentPosition);
    }

    private void RotateBasket()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        _currentPosition = _camera.ScreenToWorldPoint(mousePosition);

        var direction = _currentPosition - _basket.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _basket.rotation = Quaternion.Euler(0, 0, angle + 90);
    }
    
    // private void OnDrag()
    // {
    //     _endPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
    //     _distance = Vector2.Distance(_startPoint, _endPoint);
    //     _direction = (_startPoint - _endPoint).normalized;
    //     //_force = _direction * (_distance * _pushForce);
    //     
    //     //_trajectory.updateDots(_ball.Pos, _force2);
    //     
    //     Debug.DrawLine(_startPoint, _endPoint);
    // }
    
    

    private void OnMouseDown()
    {
        if (_ball == null)
        {
            return;
        }
        
        // _startPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        _isMouseDown = true;
        _trajectory.Show();
    }

    private void OnMouseUp()
    {
        if (_ball == null)
        {
            return;
        }
        
        _isMouseDown = false;
        _trajectory.Hide();
        //ball
        Shoot();
    }

    private void ResetStrips()
    {
        _currentPosition = _idlePosition.position;
        SetStrips(_currentPosition);
    }
    
    private void SetStrips(Vector3 position)
    {
        // for (int i = 0; i < _lineRenderers.Length; i++)
        // {
        //     _lineRenderers[i].SetPosition(1, position);
        // }
    
        //ball
        if (_ball)
        {
            _ball.SetPosition(position, _center.position, _ballPositionOffset);
            // var dir = position - _center.position;
            // var transform1 = _ballRb.transform;
            // transform1.position = position + dir.normalized * _ballPositionOffset;
            // transform1.right = -dir.normalized;
        }
    }

    private void CreateBall()
    {
        var ball = Instantiate(_ballPrefab);

        _ball = ball.GetComponent<Ball>();
        //_ballRb = ball.GetComponent<Rigidbody2D>();
        //_ballCol = ball.GetComponent<Collider2D>();

        //_ballCol.enabled = false;
        //_ballRb.isKinematic = true;
        _ball.DeactivateRb();
        
        

        ResetStrips();
    }

    private void Shoot()
    {
        //_ballRb.isKinematic = false;
        _ball.ActivateRb();
        Vector3 ballForce = (_currentPosition - _center.position) * (_force * -1);
        _ball.Push(ballForce);
        //_ballRb.velocity = ballForce;

        _ball = null;
        // _ballRb = null;
        // _ballCol = null;
        Invoke(nameof(CreateBall), 2);
    }
    
}
