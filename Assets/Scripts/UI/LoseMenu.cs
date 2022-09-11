using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LoseMenu : UIPanel
    {
        [SerializeField] private Transform _losePanel;
        [SerializeField] private Button _restartButton;

        private void Start()
        {
            GameManager.Instance.LoseGameEvent.AddListener(LoseGame);
            _restartButton.onClick.AddListener(Restart);
        }

        private void LoseGame()
        {
            ShowPanel(_losePanel.gameObject);
            StopTime();
        }
    
        private void Restart()
        {
            PlayTime();
            HidePanel(_losePanel.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
    }
}
