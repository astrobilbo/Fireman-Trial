using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.Quests
{
    public class QuestInitializer : MonoBehaviour
    {
        [SerializeField] private Quest quest;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject questInitializer;
        [SerializeField] private UnityEvent onQuestInitialized;
        private void Awake()
        {
            quest.Initialize(audioSource);
            questInitializer.SetActive(quest.IsAvailable());
            QuestsManager.Started += StopShowingQuests;
            QuestsManager.Finished += ShowQuestInScene;
        }

        private void OnDisable()
        {
            QuestsManager.Started -= StopShowingQuests;
            QuestsManager.Finished -= ShowQuestInScene;  
        }

        private void StopShowingQuests(Quest activeQuest)
        {
            questInitializer.SetActive(false);
        }

        private void ShowQuestInScene()
        {
            questInitializer.SetActive(quest.IsAvailable());
        }

        public void Initialize()
        {
            if (!QuestsManager.StartNewQuest(quest)) return;
            quest.Start();
            onQuestInitialized?.Invoke();
        }
    }
}
