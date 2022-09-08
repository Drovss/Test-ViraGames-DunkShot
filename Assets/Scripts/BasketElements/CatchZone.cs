using UnityEngine;
using UnityEngine.Events;

namespace BasketElements
{
    public class CatchZone : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
    
        public UnityEvent <Ball> CatchBallEvent;

        public  void ActiveCollider()
        {
            _collider.enabled = true;
        }

        public void DeactivateCollider()
        {
            _collider.enabled = false;
        }
    
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<Ball>())
            {
                CatchBallEvent.Invoke(col.gameObject.GetComponent<Ball>());
            }
        }
    }
}
