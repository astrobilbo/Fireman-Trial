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

        public void ToggleVisibility()
        {
            var visible = Mathf.Approximately(canvasGroup.alpha, 1);
            MakeVisible(!visible);
        }
        public void ToggleInteractive()
        {
            var interactable = canvasGroup.interactable;
            MakeInteractive(!interactable);
        }
        public void ToggleVisibleAndInteractive()
        {
            var interactable = canvasGroup.interactable;
            MakeVisibleAndInteractive(!interactable);
        }
    }
}