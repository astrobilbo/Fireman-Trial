using System;
using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.Quests
{
    public class QuestWin : MonoBehaviour
    {
        [SerializeField] private Quest quest;

        [SerializeField] private UnityEvent actions;

        private void OnEnable()
        {
            quest.Completed += Invoke;
        }

        private void OnDisable()
        {
            quest.Completed -= Invoke;
        }

        private void Invoke()
        {
            Debug.Log(quest.name + ": win");
            actions?.Invoke();
        }
    }
}
