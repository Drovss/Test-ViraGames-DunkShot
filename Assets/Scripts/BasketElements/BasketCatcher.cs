using UnityEngine;

namespace BasketElements
{
    public class BasketCatcher: IBasketBehavior
    {
        private readonly Basket _basket;
        public BasketCatcher(Basket basket)
        {
            _basket = basket;
        }
        public void Enter()
        {
            _basket.CatchZone.ActiveCollider();
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
        
        }
    }
}