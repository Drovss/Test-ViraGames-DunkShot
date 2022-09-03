using UnityEngine;

public class Trajectory : MonoBehaviour
{
   [SerializeField] private Transform _dotsParent;
   [SerializeField] private GameObject _dotPrefab;
   [SerializeField] private int _dotsNumber;
   [SerializeField] private float _dotSpacing;
   [SerializeField] [Range(0.01f, 0.1f)] private float _dotMinScale;
   [SerializeField] [Range(0.1f, 0.3f)] private float _dotMaxScale;

   private Transform[] _dotsList;
   private Vector2 _position;
   private float _timeStamp;

   private void Start()
   {
      Hide();
      PrepareDots();
   }

   public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
   {
      _timeStamp = _dotSpacing;
      for (int i = 0; i < _dotsNumber; i++)
      {
         _position.x = (ballPos.x + forceApplied.x * _timeStamp);
         _position.y = (ballPos.y + forceApplied.y * _timeStamp) -
                       (Physics2D.gravity.magnitude * _timeStamp * _timeStamp) / 2f;

         _dotsList[i].position = _position;
         _timeStamp += _dotSpacing;
      }
   }

   private void PrepareDots()
   {
      _dotsList = new Transform[_dotsNumber];
      _dotPrefab.transform.localScale = Vector3.one * _dotMaxScale;

      float scale = _dotMaxScale;
      float scaleFactor = scale / _dotsNumber;
      
      for (int i = 0; i < _dotsNumber; i++)
      {
         _dotsList[i] = Instantiate(_dotPrefab, _dotsParent).transform;
         
         _dotsList[i].localScale = Vector3.one * scale;
         if (scale > _dotMinScale)
         {
            scale -= scaleFactor;
         }
      }
   }

   public void Show()
   {
      _dotsParent.gameObject.SetActive(true);
   }
   
   public void Hide()
   {
      _dotsParent.gameObject.SetActive(false);
   }
}
