using System;
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
        private int _activeIndex;
        private void Awake()
        {
            _settings = FindAnyObjectByType<Settings>();
            if (_settings == null)
            {
                Debug.LogWarning("AudioSettingsUGUI: No settings found.", this);
                return;
            }
            
            frameRateDropdown.options.Clear();
            for (var index = 0; index < frameRates.Count; index++)
            {
                var frameRateOption = frameRates[index];
                var frameRateOptionText = frameRateOption.ToString();
                if (frameRateOption == 500)
                {
                    frameRateOptionText = "Ilimitado";
                }
                frameRateDropdown.options.Add(new TMP_Dropdown.OptionData(frameRateOptionText));
            }
            frameRateDropdown.onValueChanged.AddListener(ChangeFrameRate);
            _settings.OnFraneRateChanged += RefreshDropdown;
        }

        private void Start() => RefreshDropdown(_settings.GetFrameRate());

        private void OnDisable() => _settings.OnFraneRateChanged -= RefreshDropdown;

        private void RefreshDropdown(int frameRate)
        {
            _activeIndex=frameRates.FindIndex(f => f == frameRate);
            frameRateDropdown.value = _activeIndex;
            frameRateDropdown.RefreshShownValue();
        }

        private void ChangeFrameRate(int index)
        {
            if (index == _activeIndex) return;
            _settings.ChangeFrameRate(frameRates[index]);
        }
    }
}
