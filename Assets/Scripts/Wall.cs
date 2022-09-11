using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private Transform _parentWall;
    [SerializeField] private Transform _leftWall;
    [SerializeField] private Transform _rightWall;
    [SerializeField] private Transform _downWall;
    
    private Vector2 _minScreenPos;
    private Vector2 _maxScreenPos;
    private Camera Camera => Camera.main;
    void Start()
    {
        SetWallPosition();
    }

    private void SetWallPosition()
    {
        _minScreenPos = Camera.ViewportToWorldPoint(new Vector2(0, 0));
        _maxScreenPos = Camera.ViewportToWorldPoint(new Vector2(1, 1));

        var left = _leftWall.position;
        _leftWall.localPosition = new Vector3(_minScreenPos.x, left.y, left.z);
        var right = _rightWall.position;
        _rightWall.localPosition = new Vector3(_maxScreenPos.x, right.y, right.z);
        var down = _downWall.position;
        _downWall.localPosition = new Vector3(down.x, _minScreenPos.y, down.z);
    }

    private void LateUpdate()
    {
        GameManager.Instance.UpdatePosition(_parentWall);
    }
}
