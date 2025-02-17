using FiremanTrial.InteraciveObjects;
using UnityEngine;

namespace FiremanTrial.Quests
{
    public class QuestStepInteraction : MonoBehaviour
    {
        [SerializeField] private InteractiveObject interactiveObject;
        [SerializeField] private QuestSteps questStep;
        private void OnEnable()
        {
            if (interactiveObject != null) interactiveObject.StartInteractionActions += CompleteStep;
            if (interactiveObject != null) interactiveObject.StartBoolInteractionActions += CompleteStep;

        }

        private void OnDisable()
        {
            if (interactiveObject != null) interactiveObject.StartInteractionActions -= CompleteStep;
            if (interactiveObject != null) interactiveObject.StartBoolInteractionActions -= CompleteStep;
        }
        private void CompleteStep()
        {
            questStep?.Complete();
        }
        private void CompleteStep(bool ignore)
        {
            questStep?.Complete();
        }
    }
}
