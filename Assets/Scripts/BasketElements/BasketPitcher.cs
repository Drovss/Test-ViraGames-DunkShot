namespace BasketElements
{
    public class BasketPitcher: IBasketBehavior

    {
        private Basket _basket;
        public BasketPitcher(Basket basket)
        {
            _basket = basket;
        }
        public void Enter()
        {
            _basket.DeactivateCatcherCollider();
            _basket.CreateBall();
            _basket.Subscribe();
        }

        public void Exit()
        {
            _basket.Unsubscribe();
        }

        public void Update()
        {
            
        }
    
    
    }
}