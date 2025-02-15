using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.InteraciveObjects
{
    public class InteractiveObjectAction : MonoBehaviour
    {
        [SerializeField] private InteractiveObject interactiveObject;
        [SerializeField] private UnityEvent action;

        private void OnEnable() => SetObserver();
        private void OnDisable() => RemoveObserver();

        private void SetObserver()
        {
            if (interactiveObject is null) return;
            interactiveObject.StartInteractionActions += Invoke;
        }

        private void RemoveObserver()
        {
            if (interactiveObject is null) return;
            interactiveObject.StartInteractionActions -= Invoke;
        }

        private void Invoke()
        {
            action?.Invoke();
        }
    }
}