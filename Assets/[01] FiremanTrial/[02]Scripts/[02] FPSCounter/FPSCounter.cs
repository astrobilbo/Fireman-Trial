using TMPro;
using UnityEngine;
using System.Collections;

namespace FiremanTrial
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private bool fpsEnabled = true;
        [SerializeField] private float displayRate=0.1f;
        [SerializeField] private TextMeshProUGUI text;
        private WaitForSeconds _waitForSeconds;

        
        void Start()
        {
            if (text == null)
            {
                Debug.LogWarning("FPS Counter: TextMeshProUGUI is not assigned.",this);
                return;
            }
            
            _waitForSeconds = new WaitForSeconds(displayRate);
            StartCoroutine(UpdateFPSCounter());
        }
        
        [ContextMenu("Update display rate")]
        void UpdatedisplayRate()
        {
            _waitForSeconds = new WaitForSeconds(displayRate);
        }
        
        private IEnumerator UpdateFPSCounter()
        {
            while (fpsEnabled)
            {
                text.text = $"FPS: {(int)(1f / Time.unscaledDeltaTime)}";
                yield return _waitForSeconds;
            }
        }
    }
}
