using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.Settings.UI
{
    public class FullScreenSettingsUGUI : MonoBehaviour
    {
        [SerializeField] private  Toggle fullscreenToggle;

        private Settings _settings;

        private void Start()
        {
            _settings = FindAnyObjectByType<Settings>();
            if (_settings == null)
            {
                Debug.LogWarning("AudioSettingsUGUI: No settings found.", this);
                return;
            }
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
            _settings.OnFullScreenChanged += ToggleIsOn;
        }

        private void OnDisable() => _settings.OnFullScreenChanged -= ToggleIsOn;
        
        private void ToggleIsOn(bool isOn)
        {
            fullscreenToggle.isOn = isOn;
        }

        private void SetFullscreen(bool isFullscreen)
        {
            _settings.ChangeFullScreen(isFullscreen);
        }
    }
}
