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
        private readonly string _scoreStarsKey= "ScoreStars";

        private void Start()
        {
            _scoreStars = PlayerPrefs.GetInt(_scoreStarsKey, 0);
            PrintScoreStars();
            
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
            PlayerPrefs.SetInt(_scoreStarsKey, _scoreStars);
            PrintScoreStars();
        }

        private void PrintScoreStars()
        {
            _scoreStarsText.SetText(_scoreStars.ToString());
        }
    }
}
