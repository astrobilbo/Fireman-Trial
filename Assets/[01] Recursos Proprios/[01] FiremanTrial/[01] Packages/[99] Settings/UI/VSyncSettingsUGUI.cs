using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.Settings.UI
{
    public class VSyncSettingsUGUI : MonoBehaviour
    {            
        [SerializeField] private Toggle vsyncToggle;

        private Settings _settings;
        private bool _value;

        private void Awake()
        {

            _settings = FindAnyObjectByType<Settings>();
            if (_settings == null)
            {
                Debug.LogWarning("AudioSettingsUGUI: No settings found.", this);
                return;
            }

            _value = QualitySettings.vSyncCount != 0;
            vsyncToggle.isOn = _value;
            vsyncToggle.onValueChanged.AddListener(SetVSync);
            _settings.OnVSyncChanged += VsyncToggle;
        }
        

        private void OnDisable() => _settings.OnVSyncChanged -= VsyncToggle;


        private void VsyncToggle(int value)
        {
            _value= value != 0;
            vsyncToggle.isOn = _value;
        }
        private void SetVSync(bool active)
        {
            if (_value == active) return;
            _settings.ChangeVSync(active?1:0);
        }
    }
}
