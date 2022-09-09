using System.Threading.Tasks;
using UnityEngine;

namespace BasketElements
{
    public class BasketPitcher: IBasketBehavior
    {
        private readonly Basket _basket;
        public BasketPitcher(Basket basket)
        {
            _basket = basket;
        }
        public void Enter()
        {
            _basket.CatchZone.DeactivateCollider();
            CreateBall();
            Subscribe();
        }

        public void Exit()
        {
            Unsubscribe();
        }

        public void Update()
        {
            
        }

        private void Subscribe()
        {
            GameManager.Instance.StartMoveEvent.AddListener(ShowTrajectory);
            GameManager.Instance.PushEvent.AddListener(Shoot);
            GameManager.Instance.UpdateTrajectoryEvent.AddListener(UpdateTrajectory);
            GameManager.Instance.UpdateTrajectoryEvent.AddListener(RotateBasket);
        }

        private void Unsubscribe()
        {
            GameManager.Instance.StartMoveEvent.RemoveListener(ShowTrajectory);
            GameManager.Instance.PushEvent.RemoveListener(Shoot);
            GameManager.Instance.UpdateTrajectoryEvent.RemoveListener(UpdateTrajectory);
            GameManager.Instance.UpdateTrajectoryEvent.RemoveListener(RotateBasket);
        }
        
        private void ShowTrajectory()
        {
            if (!_basket.Ball) return;
        
            _basket.Trajectory.Show();
        }

        private void UpdateTrajectory(Vector2 force)
        {
            if (!_basket.Ball) return;
        
            _basket.Trajectory.UpdateDots(_basket.Ball.transform.position, force);
        }

        private void RotateBasket(Vector2 direction)
        {
            if (!_basket.Ball) return;
        
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _basket.BasketTransform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        private void CreateBall()
        {
            if (_basket.Ball) return;
            
            var ball = Object.Instantiate(_basket.BallPrefab);
            _basket.Ball = ball.GetComponent<Ball>();
            _basket.Ball.DeactivateRb();
            _basket.Ball.SetPosition(_basket.IdlePosition.position);
            _basket.Ball.transform.parent = _basket.BasketTransform;

            GameManager.Instance.Ball = _basket.Ball;
        }
        
        private async void Shoot(Vector2 force)
        {
            if (!_basket.Ball) return;
            
            _basket.Trajectory.Hide();
            _basket.Ball.transform.parent = null;
            _basket.Ball.Push(force);
            _basket.Ball = null;

            await Task.Delay(1000);
            _basket.StateMachine.SetBehaviorCatcher();
        }
    }
}