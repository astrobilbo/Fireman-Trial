using System;
using UnityEngine;
using UnityEngine.Audio;

namespace FiremanTrial.Settings
{
    //ToDo Add Mouse/Joystick Sensibility change value
    public class Settings : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private SettingsData data;

        //Actions → Eventos próprios das configurações, nos quais os códigos de UI se inscrevem para serem atualizados com as mudanças nos valores.
        public Action<VolumeType,int> OnVolumeChanged; 
        public Action<int> OnFraneRateChanged;
        public Action<int> OnGraphicsQualityChanged;
        public Action<int> OnVSyncChanged;
        public Action<bool> OnFullScreenChanged;
        public Action<int> OnSensibilityChanged;


        private void Awake()
        {
            data.fullScreen = Screen.fullScreen;
            data.graphicsQuality = QualitySettings.GetQualityLevel();
            data.vSync = QualitySettings.vSyncCount;
            data.mouseSensibility = 30;
            if (data.frameRate == 0) data.frameRate = 500;
            LoadData();
           
        }

        private void TriggerSettingCallbacks() // → Atualiza a UI com os dados carregados
        {
            OnVolumeChanged?.Invoke(VolumeType.MasterSound,data.masterSoundVolume); 
            OnVolumeChanged?.Invoke(VolumeType.MusicSound,data.musicSoundVolume);
            OnVolumeChanged?.Invoke(VolumeType.FxSound,data.fxSoundVolume);
            OnFraneRateChanged?.Invoke(data.frameRate);
            OnGraphicsQualityChanged?.Invoke(data.graphicsQuality);
            OnFullScreenChanged?.Invoke(data.fullScreen);
            OnVSyncChanged?.Invoke(data.vSync);
        }
        
        private void ApplyInitialSettings()
        {
            SetVolume(VolumeType.MasterSound, data.masterSoundVolume);
            SetVolume(VolumeType.MusicSound, data.musicSoundVolume);
            SetVolume(VolumeType.FxSound, data.fxSoundVolume);
            Application.targetFrameRate = data.frameRate;
            QualitySettings.SetQualityLevel(data.graphicsQuality);
            Screen.fullScreen = data.fullScreen;
            Screen.fullScreenMode = data.fullScreen? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            Application.targetFrameRate = data.frameRate;
        }

        public int GetVolume(VolumeType key)
        {
            switch (key)
            {
                case VolumeType.MasterSound:
                    return data.masterSoundVolume;
                case VolumeType.MusicSound:
                    return data.musicSoundVolume;
                case VolumeType.FxSound:
                    return data.fxSoundVolume;
                default:
                    Debug.LogError($"VolumeType {key} not found, defaulting to {VolumeType.MasterSound}");
                    return data.masterSoundVolume;
            }
        }

        public void ChangeVolume(VolumeType key, int volume)
        {
            switch (key)
            {
                case VolumeType.MasterSound when !Mathf.Approximately(volume, data.masterSoundVolume):
                    SetVolume(VolumeType.MasterSound, data.masterSoundVolume = volume);
                    break;
                case VolumeType.MusicSound when !Mathf.Approximately(volume, data.musicSoundVolume):
                    SetVolume(VolumeType.MusicSound, data.musicSoundVolume = volume);
                    break;
                case VolumeType.FxSound when !Mathf.Approximately(volume, data.fxSoundVolume):
                    SetVolume(VolumeType.FxSound, data.fxSoundVolume = volume);
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
            if (data.graphicsQuality == quality) return;
            data.graphicsQuality = quality;
            QualitySettings.SetQualityLevel(data.graphicsQuality);
            SaveData();
            OnGraphicsQualityChanged?.Invoke(data.graphicsQuality);
            OnVSyncChanged?.Invoke(QualitySettings.vSyncCount);
        }

        public void ChangeFullScreen(bool isFullScreen)
        {
            if (data.fullScreen  == isFullScreen) return;
            data.fullScreen = isFullScreen;
            Screen.fullScreen = data.fullScreen;
            Screen.fullScreenMode = data.fullScreen? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            SaveData();
            OnFullScreenChanged?.Invoke(data.fullScreen);
        }

        public void ChangeVSync(int value)
        {
            if (data.vSync == value) return;
            data.vSync = value;
            QualitySettings.vSyncCount=data.vSync;
            SaveData();
             OnVSyncChanged?.Invoke(data.vSync);
        }

        public void ChangeFrameRate(int value)
        {
            if (data.frameRate == value) return;
            Debug.Log(value);
            data.frameRate=value;
            Application.targetFrameRate = data.frameRate;
            SaveData();
            OnFraneRateChanged?.Invoke(data.frameRate);
        }
        
        public int GetFrameRate()
        {
            return data.frameRate;
        }

        public int GetQualityIndex()
        {
            return data.graphicsQuality;
        }

        public void ChangeSensibility(int value)
        {
            if (data.mouseSensibility == value) return;
            Debug.Log(value);
            data.mouseSensibility = value;
            SaveData();
            OnSensibilityChanged?.Invoke(data.mouseSensibility);
        }

        public int GetSensibility()
        {
            return data.mouseSensibility;
        }
        
        private void SaveData()
        {
            PermanentData.Save(data, nameof(Settings));
        }
        
        private void LoadData()
        {
            data= PermanentData.Load(data, nameof(Settings));
            ApplyInitialSettings();
            TriggerSettingCallbacks();
        }
        
        [ContextMenu("CleanData")]
        public void CleanData()
        {
            PermanentData.Clean();
        }

        
    }
    [Serializable]
    public struct SettingsData 
    {
        public const float MinDb = -80f;
        public const float MaxDb = 0f;
        // String com os nomes usados no audioMixer
        private const string MasterSoundKey = "MasterSound";
        private const string MusicSoundKey = "MusicSound";
        private const  string FXSoundKey= "FXSound";
        // volume de cada perfil do audioMixer
        [Range(0,20)] public int masterSoundVolume;
        [Range(0,20)] public int musicSoundVolume;
        [Range(0,20)] public int fxSoundVolume;
        public int frameRate;
        public int graphicsQuality;
        public int vSync;
        public bool fullScreen;
        public int mouseSensibility;

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
