using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private void LateUpdate()
    {
        var cameraY = GameManager.Instance.Ball.transform.position.y;
        if (cameraY > _camera.position.y)
        {
            var position = _camera.position;
            position = new Vector3(position.x, cameraY, position.z);
            _camera.position = position;
        }
    }
}
