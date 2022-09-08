using System;
using UnityEngine;

namespace BasketElements
{
    public class Basket : MonoBehaviour
    {
        public GameObject BallPrefab;
        public Transform IdlePosition;
        public Transform BasketTransform;
        public Trajectory Trajectory;
        public CatchZone CatchZone;

        [NonSerialized] public Ball Ball;
        public StateMachine StateMachine;

        private void Awake()
        {
            StateMachine = new StateMachine(this);
        }

        private void OnEnable()
        {
            CatchZone.CatchBallEvent.AddListener(CatchBall);
        }

        private void OnDisable()
        {
            CatchZone.CatchBallEvent.RemoveListener(CatchBall);
        }
        
        private void CatchBall(Ball ball)
        {
            Ball = ball;
            CatchZone.DeactivateCollider();
            Ball.DeactivateRb();
            Ball.SetPosition(IdlePosition.position);
            Ball.transform.parent = BasketTransform;
            
            StateMachine.SetBehaviorPitcher();
        }
    }
}


