using FiremanTrial.InteraciveObjects;
using UnityEngine;

namespace FiremanTrial.Quests
{
    public class QuestStepInteraction : MonoBehaviour
    {
        [SerializeField] private InteractiveObject interactiveObject;
        [SerializeField] private QuestSteps questStep;

        private void Awake()
        {
            if (questStep == null) Debug.LogWarning("No quest step assigned!", this);
            if (interactiveObject == null) Debug.LogWarning("No interactive object assigned!", this);
        }

        private void OnEnable()
        {
            if (interactiveObject != null) interactiveObject.StartInteractionActions += CompleteStep;
        }

        private void OnDisable()
        {
            if (interactiveObject != null) interactiveObject.StartInteractionActions -= CompleteStep;
        }

        private void CompleteStep()
        {
            questStep?.Complete();
        }
    }
}
