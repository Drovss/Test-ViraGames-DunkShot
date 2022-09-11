using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : UIPanel
    {
        [SerializeField] private Transform _menuPanel;
        [SerializeField] private Transform _uIPanel;
        [SerializeField] private Button _startButton;

        private void Start()
        {
            _startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            HidePanel(_menuPanel.gameObject);
            ShowPanel(_uIPanel.gameObject);
            
            GameManager.Instance.StartGameEvent.Invoke();
        }
    }
}
