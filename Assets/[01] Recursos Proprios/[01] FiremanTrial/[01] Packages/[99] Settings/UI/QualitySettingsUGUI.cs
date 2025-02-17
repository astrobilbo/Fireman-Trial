using TMPro;
using UnityEngine;

namespace FiremanTrial.Settings.UI
{
    public class QualitySettingsUGUI : MonoBehaviour
    {
        private Settings _settings;
        [SerializeField] private TMP_Dropdown qualityDropdown;
        private int _activeIndex;
        private void Start()
        {
            _settings = FindAnyObjectByType<Settings>();
            if (_settings == null)
            {
                Debug.LogWarning("AudioSettingsUGUI: No settings found.", this);
                return;
            }

            qualityDropdown.options.Clear();
            for (var index = 0; index < QualitySettings.names.Length; index++)
            {
                var qualitySettingName = QualitySettings.names[index];
                
                qualityDropdown.options.Add(new TMP_Dropdown.OptionData(qualitySettingName));
            }
            
            qualityDropdown.onValueChanged.AddListener(ChangeQuality);
            _settings.OnGraphicsQualityChanged += RefreshDropdown;
            RefreshDropdown(_settings.GetQualityIndex());
        }

        private void OnDisable() => _settings.OnGraphicsQualityChanged -= RefreshDropdown;

        private void RefreshDropdown(int quality)
        {
            _activeIndex = quality;
            qualityDropdown.value = _activeIndex;
            qualityDropdown.RefreshShownValue();
        }

        private void ChangeQuality(int index)
        {
            if (index == _activeIndex) return;
            _settings.ChangeGraphicsQuality(index);
        }
    }
}