using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private Transform _idlePosition;
    [SerializeField] private float _maxLength;
    [SerializeField] private Trajectory _trajectory;
    [SerializeField] private Transform _basket;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private float _ballPositionOffset;
    [SerializeField] private float _force;

    private Ball _ball;
    private bool _isMouseDown;
    private Vector3 _currentPosition;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;

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


    private void OnMouseDown()
    {
        if (_ball == null)
        {
            return;
        }

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
        Shoot();
    }

    private void ResetStrips()
    {
        _currentPosition = _idlePosition.position;
        SetStrips(_currentPosition);
    }
    
    private void SetStrips(Vector3 position)
    {
        if (_ball)
        {
            _ball.SetPosition(position, _center.position, _ballPositionOffset);
        }
    }

    private void CreateBall()
    {
        var ball = Instantiate(_ballPrefab);
        _ball = ball.GetComponent<Ball>();
        _ball.DeactivateRb();
        ResetStrips();
    }

    private void Shoot()
    {
        _ball.ActivateRb();
        Vector3 ballForce = (_currentPosition - _center.position) * (_force * -1);
        _ball.Push(ballForce);
        _ball = null;
        Invoke(nameof(CreateBall), 2);
    }
}
