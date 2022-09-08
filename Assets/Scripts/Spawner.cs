using BasketElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _basketPrefab;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _stepVertical;
    [SerializeField] private float _stepHorizontal;


    private float _currentVerticalPosition;
    private void Start()
    {
        _currentVerticalPosition = _startPosition.position.y;
        SpawnBasket();
    }

    private void SpawnBasket()
    {
        var position = _startPosition.position;
        var basket = Instantiate(_basketPrefab, position, Quaternion.identity).GetComponent<Basket>();
        basket.StateMachine.SetBehaviorPitcher();
        
        _currentVerticalPosition = position.y + _stepVertical;
        position = new Vector3(Random.Range(-_stepHorizontal, _stepHorizontal), _currentVerticalPosition, position.z);
        _currentVerticalPosition = position.y;
        basket = Instantiate(_basketPrefab, position, Quaternion.identity).GetComponent<Basket>();
        basket.StateMachine.SetBehaviorCatcher();
    }
}
