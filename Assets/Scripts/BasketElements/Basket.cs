using UnityEngine;

namespace BasketElements
{
    public class Basket : MonoBehaviour
    {
        [SerializeField] private GameObject _ballPrefab;
        [SerializeField] private Transform _idlePosition;
        [SerializeField] private Transform _basket;
        [SerializeField] private Trajectory _trajectory;
        [SerializeField] private CatchZone _catchZone;

        public StateMachine StateMachine;
    
        private Ball _ball;


        private void OnEnable()
        {
            // GameManager.Instance.StartMoveEvent.AddListener(ShowTrajectory);
            // GameManager.Instance.PushEvent.AddListener(Shoot);
            // GameManager.Instance.UpdateTrajectoryEvent.AddListener(UpdateTrajectory);
            // GameManager.Instance.UpdateTrajectoryEvent.AddListener(RotateBasket);
        
            _catchZone.CatchBallEvent.AddListener(CatchBall);
        }
    
        private void OnDisable()
        {
            // GameManager.Instance.StartMoveEvent.RemoveListener(ShowTrajectory);
            // GameManager.Instance.PushEvent.RemoveListener(Shoot);
            // GameManager.Instance.UpdateTrajectoryEvent.RemoveListener(UpdateTrajectory);
            // GameManager.Instance.UpdateTrajectoryEvent.RemoveListener(RotateBasket);
        
            _catchZone.CatchBallEvent.RemoveListener(CatchBall);
        }

        private void Awake()
        {
            StateMachine = new StateMachine(this);
        }

        public void Subscribe()
        {
            GameManager.Instance.StartMoveEvent.AddListener(ShowTrajectory);
            GameManager.Instance.PushEvent.AddListener(Shoot);
            GameManager.Instance.UpdateTrajectoryEvent.AddListener(UpdateTrajectory);
            GameManager.Instance.UpdateTrajectoryEvent.AddListener(RotateBasket);
        
            //_catchZone.CatchBallEvent.AddListener(CatchBall);
        }

        public void Unsubscribe()
        {
            GameManager.Instance.StartMoveEvent.RemoveListener(ShowTrajectory);
            GameManager.Instance.PushEvent.RemoveListener(Shoot);
            GameManager.Instance.UpdateTrajectoryEvent.RemoveListener(UpdateTrajectory);
            GameManager.Instance.UpdateTrajectoryEvent.RemoveListener(RotateBasket);
        
            //_catchZone.CatchBallEvent.RemoveListener(CatchBall);
        }

        public void ActiveCatcherCollider()
        {
            _catchZone.ActiveColliderDelay();
        }
    
        public void DeactivateCatcherCollider()
        {
            _catchZone.DeactivateCollider();
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

        public void CreateBall()
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
        
            //StateMachine.SetBehaviorCatcher();
        }

        private void CatchBall(Ball ball)
        {
            _ball = ball;
            _ball.DeactivateRb();
            _catchZone.DeactivateCollider();
            _ball.SetPosition(_idlePosition.position);
        
            //StateMachine.SetBehaviorPitcher();
        }
    }
}


