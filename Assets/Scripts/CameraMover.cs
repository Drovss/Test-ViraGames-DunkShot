using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private void LateUpdate()
    {
        GameManager.Instance.UpdatePosition(_camera);
    }
}
