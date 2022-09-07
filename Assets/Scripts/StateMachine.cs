using System;
using System.Collections.Generic;

public class StateMachine
{
    private Basket _basket;
    private Dictionary<Type, IBasketBehavior> _behaviors;
    private IBasketBehavior _behaviorCurrent;

    public StateMachine(Basket basket)
    {
        _basket = basket;
        InitBehaviors();
        SetBehaviorByDefault();
    }

    private void InitBehaviors()
    {
        _behaviors = new Dictionary<Type, IBasketBehavior>();
        
        _behaviors[typeof(BasketCatcher)] = new BasketCatcher(_basket);
        _behaviors[typeof(BasketPitcher)] = new BasketPitcher(_basket);
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