using UnityEngine;

namespace FiremanTrial.Quests
{
    public class QuestStepTarget : MonoBehaviour
    {
        [SerializeField] private QuestSteps questStep;
        [SerializeField] private GameObject entryObject;
        private void Start()
        {
            questStep.Started += Active;
            questStep.Completed += Disable;
            questStep.Failed += Disable;
            entryObject.SetActive(false);
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
            entryObject.SetActive(true);
        }

        private void Disable()
        {
            entryObject.SetActive(false);
        }
    }
}
