using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : UIPanel
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Transform _uIPanel;
        [SerializeField] private Transform _pausePanel;
        [SerializeField] private Transform _settingsPanel;

        private void Awake()
        {
            _pauseButton.onClick.AddListener(EnablePause);
            _resumeButton.onClick.AddListener(DisablePause);
            _settingsButton.onClick.AddListener(ShowSettings);
        }

        private void EnablePause()
        {
            StopTime();
            ShowPanel(_pausePanel.gameObject);
            HidePanel(_uIPanel.gameObject);
        }
        
        private void DisablePause()
        {
            PlayTime();
            HidePanel(_pausePanel.gameObject);
            ShowPanel(_uIPanel.gameObject);
        }

        private void ShowSettings()
        {
            HidePanel(_pausePanel.gameObject);
            ShowPanel(_settingsPanel.gameObject);
        }
    }
}
