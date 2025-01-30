using UnityEngine;

namespace FiremanTrial
{
    public static class CanvasGroupManager
    {
        public static void VisibleAndInteractive(bool isActive, CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = isActive ? 1 : 0;
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }
        public static void Visible(bool isActive, CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = isActive ? 1 : 0;
        }
        public static void Interactive(bool isActive, CanvasGroup canvasGroup)
        {
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }
    }
}