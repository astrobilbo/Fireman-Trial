using UnityEngine;

namespace FiremanTrial.Quests
{
    public class QuestStepTarget : MonoBehaviour
    {
        [SerializeField] private QuestSteps questStep;

        private void Awake()
        {
            questStep.Started += Active;
            questStep.Completed += Disable;
            questStep.Failed += Disable;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            questStep.Started -= Active;
            questStep.Completed -= Disable;           
            questStep.Failed -= Disable;
        }

        public void StepCompleted()
        {
            questStep.Complete();
        }
        
        private void Active()
        {
            gameObject.SetActive(true);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
