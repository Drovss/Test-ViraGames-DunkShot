using UnityEngine;

public class BasketCatcher: IBasketBehavior
{
    private Basket _basket;
    public BasketCatcher(Basket basket)
    {
        _basket = basket;
    }
    public void Enter()
    {
        _basket.ActiveCatcherCollider();
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}