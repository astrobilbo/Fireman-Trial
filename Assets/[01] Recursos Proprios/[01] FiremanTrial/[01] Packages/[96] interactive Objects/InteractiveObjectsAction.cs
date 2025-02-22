using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.InteraciveObjects
{
    public class InteractiveObjectsAction : MonoBehaviour
    {
        public List<InteractiveObject> interactiveObjects;
        [SerializeField] private CanvasGroup interactionCanvasGroup;

        private void Start()
        {
            CanvasGroupManager.VisibleAndInteractive(false, interactionCanvasGroup);
            SetObserver();
        }

        private void OnDisable() => RemoveObserver();

        private void SetObserver()
        {
            interactiveObjects = new List<InteractiveObject>(FindObjectsByType<InteractiveObject>(FindObjectsSortMode.None));
            foreach (InteractiveObject interactiveObject in interactiveObjects)
            {
                interactiveObject.OnViewEnterAction += Started;
                interactiveObject.EndInteractionAction += End;
            }
        }

        private void RemoveObserver()
        {
            foreach (InteractiveObject interactiveObject in interactiveObjects)
            {
                interactiveObject.OnViewEnterAction -= Started;
                interactiveObject.EndInteractionAction -= End;
            }
        }

        private void Started()
        {
            CanvasGroupManager.VisibleAndInteractive(true, interactionCanvasGroup);
        }

        private void End()
        {
            CanvasGroupManager.VisibleAndInteractive(false, interactionCanvasGroup);
        }
    }
}