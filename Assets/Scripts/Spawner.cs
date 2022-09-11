using System.Collections.Generic;
using BasketElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _basketPrefab;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _stepVertical;
    [SerializeField] private int _countBasket;
    [SerializeField] private float _maxDistanceFromCenter;
    [SerializeField] private float _minDistanceFromCenter;

    private readonly Queue<Basket> _basketsQueue = new Queue<Basket>();
    private Vector3 _currentPosition;

    private bool _rightPosition;
    
    private void Start()
    {
        GameManager.Instance.StartGameEvent.AddListener(InitSpawner);
    }

    private void InitSpawner()
    {
        InitBaskets();
        SetBasketsPosition();
        SubscribeOnBasket();
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
        basket.Star.Activate();
        _basketsQueue.Enqueue(basket);
    }

    private Vector3 GetNewPosition()
    {
        Vector3 position;
        
        if (_rightPosition)
        {
            position = new Vector3(
                Random.Range(_minDistanceFromCenter, _maxDistanceFromCenter),
                _currentPosition.y + _stepVertical,
                _currentPosition.z);
            
            _rightPosition = false;
        }
        else
        {
            position = new Vector3(
                Random.Range(-_maxDistanceFromCenter, -_minDistanceFromCenter),
                _currentPosition.y + _stepVertical,
                _currentPosition.z);
            
            _rightPosition = true;
        }

        return position;
    }

    private void SubscribeOnBasket()
    {
        foreach (var basket in _basketsQueue)
        {
            basket.CatchBallEvent.AddListener(UpdateBasketPosition);
        }
    }

    private void UpdateBasketPosition()
    {
        if (_basketsQueue.Peek().Ball) return;
        
        SetNextBasketPosition();
        
        GameManager.Instance.BasketCatchBallEvent.Invoke();
    }
}
