using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.Settings.UI
{
    public class AudioSettingsUGUI: MonoBehaviour
    {
        [SerializeField] private  Slider slider;
        [SerializeField] private  VolumeType type;
        private const int SliderMin = 0;
        private const int SliderMax = 20;

        private float _volume;
        private Settings _settings;
        private void Awake()
        {
            _settings = FindAnyObjectByType<Settings>();
            if (_settings == null)
            {
                Debug.LogWarning("AudioSettingsUGUI: No settings found.", this);
                return;
            }
            SetSliderRange();
            slider.wholeNumbers = true;
            slider.onValueChanged.AddListener(ChangeVolume);
            slider.value = _settings.GetVolume(type);
            _settings.OnVolumeChanged += ChangeSliderValue;
        }
        
        private void OnDisable() => _settings.OnVolumeChanged -= ChangeSliderValue;

        private void SetSliderRange()
        {
            slider.minValue = SliderMin;
            slider.maxValue= SliderMax;
        }

        private void ChangeSliderValue(VolumeType key, int value)
        {
            if (key != type || Mathf.Approximately(_volume, value)) return;
            
            _volume = value;
            slider.value = _volume;
        }

        private void ChangeVolume(float value)
        {
            if (Mathf.Approximately(_volume, value)) return;
            _settings.ChangeVolume(type, (int)value);
        }
    }
}