using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.Quests
{
    public class QuestStarted : MonoBehaviour
    {
        [SerializeField] private Quest quest;

        [SerializeField] private UnityEvent actions;

        private void OnEnable()
        {
            quest.Started += Invoke;
        }

        private void OnDisable()
        {
            quest.Started -= Invoke;
        }

        private void Invoke()
        {
            actions?.Invoke();
        }
    }
}