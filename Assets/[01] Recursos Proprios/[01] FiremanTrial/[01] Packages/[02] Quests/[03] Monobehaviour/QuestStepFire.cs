using FiremanTrial.Fire;
using UnityEngine;

namespace FiremanTrial.Quests
{
    public class QuestStepFire : MonoBehaviour
    {
        [SerializeField] private FireManager fire;
        [SerializeField] private QuestSteps step;
        [SerializeField] private Quest quest;
        private void OnEnable()
        {
            fire.FireExtinguisherFail += Failed;
            fire.FireExtinguisherSuccess += Completed;
        }

        private void OnDisable()
        {
            fire.FireExtinguisherFail -= Failed;
            fire.FireExtinguisherSuccess -= Completed;
        }

        

        private void Completed()
        {
            Debug.Log("Quest Completed");
            step.Complete();
            quest.Complete();
        }


        private void Failed()
        {
            step.Fail();
            quest.Lose();
        }
    }
}
