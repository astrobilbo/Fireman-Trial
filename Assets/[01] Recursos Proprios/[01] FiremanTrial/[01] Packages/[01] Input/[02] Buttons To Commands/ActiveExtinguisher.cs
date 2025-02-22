using System.Collections.Generic;
using FiremanTrial.Extinguisher;
using UnityEngine;

namespace FiremanTrial.InputManager
{
    public class ActiveExtinguisher : MonoBehaviour
    {
        [SerializeField]private List<FireExtinguisher> fireExtinguishers;
        [SerializeField] private CanvasGroup fireExtinguisherCanvasGroup;

        private void Start()
        {
            fireExtinguishers = new List<FireExtinguisher>(FindObjectsByType<FireExtinguisher>(FindObjectsSortMode.None));
            SetObserver();
        }
        private void OnDisable() => RemoveObserver();

        private void SetObserver()
        {
            foreach (var interactiveObject in fireExtinguishers)
            {
                interactiveObject.InHand += Started;
                interactiveObject.OutHand += End;
            }
        }

        private void RemoveObserver()
        {
            foreach (var interactiveObject in fireExtinguishers)
            {
                interactiveObject.InHand -= Started;
                interactiveObject.OutHand -= End;
            }
        }
        
        public void Active()
        {
            foreach (var fireExtinguisher in fireExtinguishers)
            {
                fireExtinguisher.ActiveExtinguisher();
            }
        }
        public void Desactive()
        {
            foreach (var fireExtinguisher in fireExtinguishers)
            {
                fireExtinguisher.DesactiveExtinguisher();
            }
        }
        
        private void Started()
        {
            CanvasGroupManager.VisibleAndInteractive(true, fireExtinguisherCanvasGroup);
        }

        private void End()
        {
            CanvasGroupManager.VisibleAndInteractive(false, fireExtinguisherCanvasGroup);
        }
    }
}