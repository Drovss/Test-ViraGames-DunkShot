using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Settings : UIPanel
    {
        [SerializeField] private Transform _settingsPanel;
        [SerializeField] private Transform _pausePanel;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Button _okButton;
        [SerializeField] private Camera _camera;
        [SerializeField] private Color _colorBlackTheme;
        [SerializeField] private Color _colorWhiteTheme;

        private bool _enabledBlackTheme;
        private readonly string _enabledBlackThemeKey = "EnabledBlackTheme";

        private void Awake()
        {
            _enabledBlackTheme = PlayerPrefs.GetInt(_enabledBlackThemeKey, 0) != 0;
            _toggle.isOn = _enabledBlackTheme;
            UpdateFon();
            _okButton.onClick.AddListener(HideAndSaveSettings);
        }

        private void HideAndSaveSettings()
        {
            UpdateFon();
            HidePanel(_settingsPanel.gameObject);
            ShowPanel(_pausePanel.gameObject);
        }

        private void UpdateFon()
        {
            PlayerPrefs.SetInt(_enabledBlackThemeKey, _toggle.isOn? 1 : 0);
            _camera.backgroundColor = _toggle.isOn ? _colorBlackTheme : _colorWhiteTheme;
        }
    }
}
