using System;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private CatchZone _catchZone;
    [SerializeField] private Transform _idlePosition;
    [SerializeField] private Transform _basket;
    [SerializeField] private Trajectory _trajectory;

    private Ball _ball;

    private BasketState _currentState;

    private void OnEnable()
    {
        GameManager.Instance.StartMoveEvent.AddListener(ShowTrajectory);
        GameManager.Instance.PushEvent.AddListener(Shoot);
        GameManager.Instance.UpdateTrajectotyEvent.AddListener(UpdateTrajectory);
        GameManager.Instance.UpdateTrajectotyEvent.AddListener(RotateBasket);
        
        _catchZone.CatchBallEvent.AddListener(CatchBall);
    }

    private void OnDisable()
    {
        GameManager.Instance.StartMoveEvent.RemoveListener(ShowTrajectory);
        GameManager.Instance.PushEvent.RemoveListener(Shoot);
        GameManager.Instance.UpdateTrajectotyEvent.RemoveListener(UpdateTrajectory);
        GameManager.Instance.UpdateTrajectotyEvent.RemoveListener(RotateBasket);
        
        _catchZone.CatchBallEvent.RemoveListener(CatchBall);
    }

    private void Start()
    {
        InitBehaviors();
        SetBehaviorByDefault();
        
        
        if (_currentState == BasketState.Pitcher)
        {
            CreateBall();
        }
        else
        {
            
        }
    }

    public void SetStatePitcher()
    {
        _currentState = BasketState.Pitcher;
    }
    
    public void SetStateCatcher()
    {
        _currentState = BasketState.Catcher;
    }

    
    private void ShowTrajectory()
    {
        if (!_ball) return;
        
        _trajectory.Show();
    }

    private void UpdateTrajectory(Vector2 force)
    {
        if (!_ball) return;
        
        _trajectory.UpdateDots(_ball.transform.position, force);
    }

    private void RotateBasket(Vector2 direction)
    {
        if (!_ball) return;
        
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _basket.rotation = Quaternion.Euler(0, 0, angle -90 );
        _ball.transform.rotation = Quaternion.Euler(0, 0, angle -90 );
    }

    private void CreateBall()
    {
        var ball = Instantiate(_ballPrefab);
        _ball = ball.GetComponent<Ball>();
        _ball.DeactivateRb();
        _ball.SetPosition(_idlePosition.position);
    }

    private void Shoot(Vector2 force)
    {
        if (!_ball) return;
        _trajectory.Hide();
        _ball.Push(force);
        _ball = null;
        _catchZone.ActiveColliderDelay();
    }

    private void CatchBall(Ball ball)
    {
        _ball = ball;
        _ball.DeactivateRb();
        _catchZone.DeactivateCollider();
        _ball.SetPosition(_idlePosition.position);
    }

    public enum BasketState
    {
        Pitcher,
        Catcher
    }


    private Dictionary<Type, IBasketBehavior> _behaviors;
    private IBasketBehavior _behaviorCurrent;

    private void InitBehaviors()
    {
        _behaviors = new Dictionary<Type, IBasketBehavior>();
        
        _behaviors[typeof(BasketCatcher)] = new BasketCatcher();
        _behaviors[typeof(BasketPitcher)] = new BasketPitcher();
    }

    private void SetBehavior(IBasketBehavior behavior)
    {
        _behaviorCurrent?.Exit();

        _behaviorCurrent = behavior;
        _behaviorCurrent.Enter();
    }

    private void SetBehaviorByDefault()
    {
        SetBehaviorCatcher();
    }

    private IBasketBehavior GetBehavior<T>() where T : IBasketBehavior
    {
        var type = typeof(T);
        return _behaviors[type];
    }

    public void SetBehaviorCatcher()
    {
        var behavior = GetBehavior<BasketCatcher>();
        SetBehavior(behavior);
    }

    public void SetBehaviorPitcher()
    {
        var behavior = GetBehavior<BasketPitcher>();
        SetBehavior(behavior);
    }
}


