using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.Settings.UI
{
    public class MouseSensibilitySettingsUGUI: MonoBehaviour
    {
        [SerializeField] private  Slider slider;
        private const int SliderMin = 10;
        private const int SliderMax = 110;

        private float _volume;
        private Settings _settings;
        private void Start()
        {
            _settings = FindAnyObjectByType<Settings>();
            if (_settings == null)
            {
                Debug.LogWarning("AudioSettingsUGUI: No settings found.", this);
                return;
            }
            SetSliderRange();
            slider.wholeNumbers = true;
            slider.value = _settings.GetSensibility();
            slider.onValueChanged.AddListener(ChangeSensibility);
            _settings.OnSensibilityChanged += ChangeSliderValue;
        }
        
        private void OnDisable() => _settings.OnSensibilityChanged -= ChangeSliderValue;

        private void SetSliderRange()
        {
            slider.minValue = SliderMin;
            slider.maxValue= SliderMax;
        }

        private void ChangeSliderValue(int value)
        {
            if (Mathf.Approximately(_volume, value)) return;
            _volume = value;
            slider.value = _volume;
        }

        private void ChangeSensibility(float value)
        {
            if (Mathf.Approximately(_volume, value)) return;
            _settings.ChangeSensibility((int)value);
        }
    }
}