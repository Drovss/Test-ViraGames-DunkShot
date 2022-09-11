using UnityEngine;
using Random = UnityEngine.Random;

namespace BasketElements
{
    public class Star : MonoBehaviour
    {
        [SerializeField] [Range(0, 100)] private int _activationChance;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Collider2D _collider;
       
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<Ball>())
            {
                GameManager.Instance.BasketCatchStarEvent.Invoke();
            }
        }

        public void Activate()
        {
            var chance = Random.Range(0, 100);

            if (chance < _activationChance)
            {
                _renderer.enabled = true;
                _collider.enabled = true;
            }
        }

        public void Deactivate()
        {
            _renderer.enabled = false;
            _collider.enabled = false;
        }
    }
}
