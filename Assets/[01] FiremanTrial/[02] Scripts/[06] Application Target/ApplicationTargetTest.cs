#if UNITY_EDITOR
using UnityEngine;

namespace FiremanTrial
{
    public class ApplicationTargetTest : MonoBehaviour
    {
        [SerializeField] private DeviceType testDeviceType;

        public void Awake()
        {
            var applicationTargets = FindObjectsByType<ApplicationTarget>(FindObjectsSortMode.None);
            ApplicationTarget.EnableTestMode();

            foreach (var app in applicationTargets)
            {
                app.Config(testDeviceType);
            }
        }
    }
}
#endif
