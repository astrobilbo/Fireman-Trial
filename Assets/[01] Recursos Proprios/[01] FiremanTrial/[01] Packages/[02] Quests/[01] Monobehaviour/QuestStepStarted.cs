using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.Quests
{
    public class QuestStepStarted : MonoBehaviour
    {
        [SerializeField] private QuestSteps step;

        [SerializeField] private UnityEvent actions;

        private void OnEnable()
        {
            step.Completed += Invoke;
        }

        private void OnDisable()
        {
            step.Completed -= Invoke;
        }

        private void Invoke()
        {
            actions?.Invoke();
        }
    }
}