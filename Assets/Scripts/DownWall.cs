using UnityEngine;

public class DownWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Ball>())
        {
            GameManager.Instance.LoseGameEvent.Invoke();
        }
    }
}
