using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FiremanTrial.Settings.UI
{
    public class FrameRateSettingsUGUI : MonoBehaviour
    {
        private Settings _settings;
        [SerializeField] private TMP_Dropdown frameRateDropdown;
        [SerializeField] private List<int> frameRates;
        private int _activeFrameRate;

        private void Awake()
        {
            _settings = FindAnyObjectByType<Settings>();
            if (_settings == null)
            {
                Debug.LogWarning("AudioSettingsUGUI: No settings found.", this);
                return;
            }
            var frameRate = _settings.GetFrameRate();
            frameRateDropdown.options.Clear();
            for (var index = 0; index < frameRates.Count; index++)
            {
                var frameRateOption = frameRates[index];
                var frameRateOptionText = frameRateOption.ToString();
                if (frameRateOption == 500)
                {
                    frameRateOptionText = "unlimited";
                }

                if (frameRateOption == frameRate)
                {
                    _activeFrameRate=index;
                }
                frameRateDropdown.options.Add(new TMP_Dropdown.OptionData(frameRateOptionText));
            }
            frameRateDropdown.value=_activeFrameRate;
            frameRateDropdown.RefreshShownValue();
            frameRateDropdown.onValueChanged.AddListener(ChangeFrameRate);
            _settings.OnFraneRateChanged += RefreshDropdown;
        }

        private void OnDisable() => _settings.OnFraneRateChanged -= RefreshDropdown;

        private void RefreshDropdown(int frameRate)
        {
            _activeFrameRate = frameRate;
            frameRateDropdown.value = _activeFrameRate;
            frameRateDropdown.RefreshShownValue();
        }

        private void ChangeFrameRate(int index)
        {
            if (index == _activeFrameRate) return;
            _settings.ChangeFrameRate(index);
        }
    }
}
