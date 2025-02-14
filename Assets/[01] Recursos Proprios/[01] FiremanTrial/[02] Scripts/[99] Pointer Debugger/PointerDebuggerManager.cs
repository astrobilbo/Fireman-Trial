using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial
{
    public class PointerDebuggerManager : MonoBehaviour
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD

        private void Awake()
        {
            var canvas = FindAnyObjectByType<Canvas>();
            AttachToCanvas(canvas);
        }
        
        private static void AttachToCanvas(Canvas canvas)
        {
            if (!canvas) return;
            var uiElements = canvas.GetComponentsInChildren<Graphic>(true);
            foreach (var element in uiElements)
            {
                if (element.GetComponent<PointerDebugger>() == null)
                {
                    element.gameObject.AddComponent<PointerDebugger>();
                }
            }
        }

#else
        private void Awake()
        {
            Destroy(gameObject);
        }
#endif
    }
}