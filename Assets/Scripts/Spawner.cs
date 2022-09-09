using System.Collections.Generic;
using BasketElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _basketPrefab;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _stepVertical;
    [SerializeField] private float _stepHorizontal;
    [SerializeField] private int _countBasket;

    private readonly Queue<Basket> _basketsQueue = new Queue<Basket>();
    private Vector3 _currentPosition;
    
    private void Start()
    {
        InitBaskets();
        SetBasketsPosition();
        SetPitcherState();
    }

    private void SetPitcherState()
    {
        if (_basketsQueue.Count == 0) return;
        
        _basketsQueue.Peek().StateMachine.SetBehaviorPitcher();
    }

    private void InitBaskets()
    {
        for (int i = 0; i < _countBasket; i++)
        {
            var basket = Instantiate(_basketPrefab).GetComponent<Basket>();
            _basketsQueue.Enqueue(basket);
        }
    }

    private void SetBasketsPosition()
    {
        if (_basketsQueue.Count == 0) return;

        SetFirstBasketPosition(_startPosition.position);
        
        for (int i = 0; i < _countBasket - 1; i++)
        {
            SetNextBasketPosition();
        }
    }

    private void SetFirstBasketPosition(Vector3 position)
    {
        var basket = _basketsQueue.Dequeue();
        _currentPosition = position;
        basket.SetPosition(_currentPosition);
        _basketsQueue.Enqueue(basket);
    }

    private void SetNextBasketPosition()
    {
        var basket = _basketsQueue.Dequeue();
        _currentPosition = GetNewPosition();
        basket.SetPosition(_currentPosition);
        _basketsQueue.Enqueue(basket);
    }

    private Vector3 GetNewPosition()
    {
        var position = new Vector3(
            Random.Range(-_stepHorizontal, _stepHorizontal),
            _currentPosition.y + _stepVertical,
            _currentPosition.z);

        return position;
    }
}
