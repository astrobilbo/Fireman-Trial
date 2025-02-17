using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.Quests
{
    public class QuestFail : MonoBehaviour
    {
        [SerializeField] private Quest quest;

        [SerializeField] private UnityEvent actions;

        private void OnEnable()
        {
            quest.Failed += Invoke;
        }

        private void OnDisable()
        {
            quest.Failed -= Invoke;
        }

        private void Invoke()
        {
            Debug.Log(quest.name + ": fail");
            actions?.Invoke();
        }
    }
}
