using LendasEMitos.Dialogue;
using UnityEngine;

namespace FiremanTrial.Quests
{
    public class QuestStepChat : MonoBehaviour
    {
        [SerializeField] private AIConversant conversant;
        [SerializeField] private QuestSteps step;

        private void OnEnable()
        {
            step.Started += Started;
        }

        private void OnDisable()
        {
            step.Started -= Started;
        }

        private void Started()
        {
            conversant.ActiveConversation();
        }

        public void Completed()
        {
            step.Complete();
        }


        public void Failed()
        {
            step.Fail();
        }
    }
}