using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.Quests
{
    public class QuestStepFail : MonoBehaviour
    {
        [SerializeField] private QuestSteps step;

        [SerializeField] private UnityEvent actions;

        private void OnEnable()
        {
            step.Failed += Invoke;
        }

        private void OnDisable()
        {
            step.Failed -= Invoke;
        }

        private void Invoke()
        {
            actions?.Invoke();
        }
    }
}