using UnityEngine;

namespace FiremanTrial.Quests.UI
{
    public class QuestsCompleted : MonoBehaviour
    {
       [SerializeField] private CanvasGroup canvasGroup;

        private void Awake() => CanvasGroupManager.VisibleAndInteractive(false,canvasGroup);

        private void OnEnable() => QuestsManager.Win += Completed;

        private void OnDisable() => QuestsManager.Win -= Completed;

        private void Completed() => CanvasGroupManager.VisibleAndInteractive(true,canvasGroup);
    }
}
