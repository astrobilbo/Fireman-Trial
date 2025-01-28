using System;
using UnityEngine;
using UnityEngine.Audio;

namespace FiremanTrial.Settings
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField,Range(0,20)] private  int initialVolume = 10;
        
        //Actions → Eventos próprios das configurações, nos quais os códigos de UI se inscrevem para serem atualizados com as mudanças nos valores.
        public Action<VolumeType,int> OnVolumeChanged; 
        public Action<int> OnFraneRateChanged;
        public Action<int> OnGraphicsQualityChanged;
        public Action<int> OnVSyncChanged;
        public Action<bool> OnFullScreenChanged;

        private SettingsData _data;

        private void Start()
        {
            LoadData();  
            TriggerSettingCallbacks();
            ApplyInitialSettings();
        }
        
        private void TriggerSettingCallbacks() // → Atualiza a UI com os dados carregados
        {
            OnVolumeChanged?.Invoke(VolumeType.MasterSound,_data.MasterSoundVolume); 
            OnVolumeChanged?.Invoke(VolumeType.MusicSound,_data.MusicSoundVolume);
            OnVolumeChanged?.Invoke(VolumeType.FxSound,_data.FXSoundVolume);
            OnFraneRateChanged?.Invoke(_data.FrameRate);
            OnGraphicsQualityChanged?.Invoke(_data.GraphicsQuality);
            OnFullScreenChanged?.Invoke(_data.FullScreen);
            OnVSyncChanged?.Invoke(_data.VSync);
        }
        
        private void ApplyInitialSettings()
        {
            mixer.SetFloat(SettingsData.VolumeKey(VolumeType.MasterSound),DBVolume(_data.MasterSoundVolume));
            mixer.SetFloat(SettingsData.VolumeKey(VolumeType.MusicSound),DBVolume(_data.MusicSoundVolume));
            mixer.SetFloat(SettingsData.VolumeKey(VolumeType.FxSound),DBVolume(_data.FXSoundVolume));
            Application.targetFrameRate = _data.FrameRate;
            QualitySettings.SetQualityLevel(_data.GraphicsQuality);
            Screen.fullScreen = _data.FullScreen;
            Screen.fullScreenMode = _data.FullScreen? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            Application.targetFrameRate = _data.FrameRate;
        }

        public int GetVolume(VolumeType key)
        {
            switch (key)
            {
                case VolumeType.MasterSound:
                    return _data.MasterSoundVolume;
                case VolumeType.MusicSound:
                    return _data.MusicSoundVolume;
                case VolumeType.FxSound:
                    return _data.FXSoundVolume;
                default:
                    Debug.LogError($"VolumeType {key} not found, defaulting to {VolumeType.MasterSound}");
                    return _data.MasterSoundVolume;
            }
        }

        public void ChangeVolume(VolumeType key, int volume)
        {
            switch (key)
            {
                case VolumeType.MasterSound when !Mathf.Approximately(volume, _data.MasterSoundVolume):
                    SetVolume(VolumeType.MasterSound, _data.MasterSoundVolume = volume);
                    break;
                case VolumeType.MusicSound when !Mathf.Approximately(volume, _data.MusicSoundVolume):
                    SetVolume(VolumeType.MusicSound, _data.MusicSoundVolume = volume);
                    break;
                case VolumeType.FxSound when !Mathf.Approximately(volume, _data.FXSoundVolume):
                    SetVolume(VolumeType.FxSound, _data.FXSoundVolume = volume);
                    break;
                default:
                    Debug.LogError($"Key {key} not found");
                    SetVolume(VolumeType.MasterSound, _data.MasterSoundVolume = volume);
                    break;
            }
        }

        private void SetVolume(VolumeType key, int volume)
        {
            mixer.SetFloat(SettingsData.VolumeKey(key), DBVolume(volume));
            SaveData();
            OnVolumeChanged?.Invoke(key, volume);
        }
        
        private static float DBVolume(float volume) => Mathf.Lerp(SettingsData.MinDb, SettingsData.MaxDb, volume / 20f);

        public void ChangeGraphicsQuality(int quality)
        {
            if (_data.GraphicsQuality == quality) return;
            _data.GraphicsQuality = quality;
            QualitySettings.SetQualityLevel(_data.GraphicsQuality);
            SaveData();
            OnGraphicsQualityChanged?.Invoke(_data.GraphicsQuality);
            OnVSyncChanged?.Invoke(QualitySettings.vSyncCount);
        }

        public void ChangeFullScreen(bool isFullScreen)
        {
            if (_data.FullScreen  == isFullScreen) return;
            _data.FullScreen = isFullScreen;
            Screen.fullScreen = _data.FullScreen;
            Screen.fullScreenMode = _data.FullScreen? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            SaveData();
            OnFullScreenChanged?.Invoke(_data.FullScreen);
        }

        public void ChangeVSync(int value)
        {
            if (_data.VSync == value) return;
            _data.VSync = value;
            QualitySettings.vSyncCount=_data.VSync;
            SaveData();
             OnVSyncChanged?.Invoke(_data.VSync);
        }

        public void ChangeFrameRate(int value)
        {
            if (_data.FrameRate == value) return;
            _data.FrameRate=value;
            Application.targetFrameRate = _data.FrameRate;
            SaveData();
            OnFraneRateChanged?.Invoke(_data.FrameRate);
        }
        
        public int GetFrameRate()
        {
            return _data.FrameRate;
        }
        
        private void SaveData()
        {
            PermanentData.Save(_data, nameof(Settings));
        }
        
        private void LoadData()
        {
            _data = new SettingsData(initialVolume,Application.targetFrameRate, QualitySettings.GetQualityLevel(), QualitySettings.vSyncCount, Screen.fullScreen);
            PermanentData.Load(_data, nameof(Settings));
        }

        public void CleanData()
        {
            PermanentData.Clean();
        }

        
    }
    
    public struct SettingsData 
    {
        public const float MinDb = -80f;
        public const float MaxDb = 0f;
        // String com os nomes usados no audioMixer
        private const string MasterSoundKey = "MasterSound";
        private const string MusicSoundKey = "MusicSound";
        private const  string FXSoundKey= "FXSound";
        // volume de cada perfil do audioMixer
        public int MasterSoundVolume;
        public int MusicSoundVolume;
        public int FXSoundVolume;
        public int FrameRate;
        public int GraphicsQuality;
        public int VSync;
        public bool FullScreen;

        public SettingsData(int initialVolume,int frameRate, int graphicsQuality,int vSync, bool fullScreen)
        {
            MasterSoundVolume = initialVolume;
            MusicSoundVolume = initialVolume;
            FXSoundVolume = initialVolume;
            this.FrameRate = frameRate;
            this.GraphicsQuality= graphicsQuality;
            this.VSync = vSync;
            this.FullScreen = fullScreen;
        }

        public static string VolumeKey(VolumeType volumeType)
        {
            switch (volumeType)
            {
                case VolumeType.MasterSound:
                    return MasterSoundKey;
                case VolumeType.MusicSound:
                    return MusicSoundKey;
                case VolumeType.FxSound:
                    return FXSoundKey;
                default:
                    Debug.LogError($"VolumeType {volumeType} not found, defaulting to {VolumeType.MasterSound}");
                    return MasterSoundKey;
            }
        }
    }

    public enum VolumeType
    {
        MasterSound,
        MusicSound,
        FxSound,
    }
}
