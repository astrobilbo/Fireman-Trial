using TMPro;
using UnityEngine;

namespace FiremanTrial.Settings.UI
{
    public class QualitySettingsUGUI : MonoBehaviour
    {
        private Settings _settings;
        [SerializeField] private TMP_Dropdown qualityDropdown;
        private int _activeQuality;
        private void Awake()
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
                if (qualitySettingName.Contains('.'))
                {
                    _activeQuality=index;
                }
                qualityDropdown.options.Add(new TMP_Dropdown.OptionData(qualitySettingName));
            }
            
            qualityDropdown.value=_activeQuality;
            qualityDropdown.RefreshShownValue();
            qualityDropdown.onValueChanged.AddListener(ChangeQuality);
            _settings.OnGraphicsQualityChanged += RefreshDropdown;
        }

        private void OnDisable() => _settings.OnGraphicsQualityChanged -= RefreshDropdown;

        private void RefreshDropdown(int quality)
        {
            _activeQuality = quality;
            qualityDropdown.value = _activeQuality;
            qualityDropdown.RefreshShownValue();
        }

        private void ChangeQuality(int index)
        {
            if (index == _activeQuality) return;
            _settings.ChangeGraphicsQuality(index);
        }
    }
}