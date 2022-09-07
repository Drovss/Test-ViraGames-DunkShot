using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CatchZone : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Collider2D _collider;
    
    public UnityEvent <Ball> CatchBallEvent;

    // private void Start()
    // {
    //     DeactivateCollider();
    // }

    public async void ActiveColliderDelay()
    {
        await Task.Delay(Mathf.RoundToInt(_delay * 1000));
        
        _collider.enabled = true;
    }
    
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
