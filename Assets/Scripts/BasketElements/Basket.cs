using System;
using UnityEngine;
using UnityEngine.Events;

namespace BasketElements
{
    public class Basket : MonoBehaviour
    {
        public GameObject BallPrefab;
        public Transform IdlePosition;
        public Transform ResetPosition;
        public Transform BasketTransform;
        public Trajectory Trajectory;
        public CatchZone CatchZone;
        public Star Star;
        public AudioSource Audio;

        [NonSerialized] public Ball Ball;
        public StateMachine StateMachine;

        [HideInInspector] public UnityEvent CatchBallEvent;

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

        public void SetPosition(Vector3 position)
        {
            BasketTransform.position = position;
            BasketTransform.rotation = Quaternion.identity;
        }

        private void CatchBall(Ball ball)
        {
            Ball = ball;
            CatchZone.DeactivateCollider();
            Ball.DeactivateRb();
            Ball.SetPosition(IdlePosition.position);
            Ball.transform.parent = BasketTransform;
            Audio.Play();
            StateMachine.SetBehaviorPitcher();
            
            CatchBallEvent.Invoke();
        }
    }
}


