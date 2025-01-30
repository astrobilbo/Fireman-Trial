using UnityEngine;

namespace FiremanTrial
{
    public class CanvasGroupManagerUGUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        public void MakeVisibleAndInteractive(bool value)
        {
            CanvasGroupManager.VisibleAndInteractive(value, canvasGroup);
        }
        
        public void MakeVisible(bool value)
        {
            CanvasGroupManager.Visible(value, canvasGroup);
        }
        
        public void MakeInteractive(bool value)
        {
            CanvasGroupManager.Interactive(value, canvasGroup);
        }
    }
}