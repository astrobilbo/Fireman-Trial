using UnityEngine;

namespace FiremanTrial
{
    public class ApplicationTarget : MonoBehaviour
    {
        [SerializeField] private DeviceType deviceType;

        private static bool _testMode=false;

        public static void EnableTestMode()
        {
            _testMode=true;
        }

        private void Start()
        {
            if (Application.isEditor && _testMode) return;
            
            Config(deviceType);
            
        }


        public void Config( DeviceType device )
        {
            if (device != SystemInfo.deviceType)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
