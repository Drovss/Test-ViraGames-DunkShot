using TMPro;
using UnityEngine;

namespace UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _scoreStarsText;

        private int _score;
        private int _scoreStars;

        private void Start()
        {
            GameManager.Instance.BasketCatchBallEvent.AddListener(UpScore);
            GameManager.Instance.BasketCatchStarEvent.AddListener(UpScoreStars);
        }

        private void UpScore()
        {
            _score++;
            _scoreText.SetText(_score.ToString());
        }
        
        private void UpScoreStars()
        {
            _scoreStars++;
            _scoreStarsText.SetText(_scoreStars.ToString());
        }
    }
}
