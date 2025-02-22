using UnityEngine;

namespace FiremanTrial
{
    public class ApplicationTarget : MonoBehaviour
    {
        [SerializeField] private RuntimePlatform platform;
        private static bool _testMode=false;

        public static void EnableTestMode()
        {
            _testMode=true;
        }

        private void Start()
        {
            if (Application.isEditor && _testMode) return;
            
            Config(Application.platform);
            
        }


        public void Config( RuntimePlatform device )
        {
            if (device != platform)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
