using UnityEngine;

namespace FiremanTrial
{
    public class ApplicationTargetTest : MonoBehaviour
    {
#if UNITY_EDITOR
        
        [SerializeField] private RuntimePlatform testDeviceType;

        public void Awake()
        {
            var applicationTargets = FindObjectsByType<ApplicationTarget>(FindObjectsSortMode.None);
            ApplicationTarget.EnableTestMode();

            foreach (var app in applicationTargets)
            {
                app.Config(testDeviceType);
            }
        }
#endif
    }
}
