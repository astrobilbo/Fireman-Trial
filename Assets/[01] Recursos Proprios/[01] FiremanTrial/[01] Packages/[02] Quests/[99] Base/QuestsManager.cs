using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FiremanTrial.Quests
{
    public static class QuestsManager 
    {
        public static event Action<Quest> Started;
        public static event Action Finished;
        public static event Action Win;
        private static List<Quest> _quests= new List<Quest>();
        private static Quest _activeQuest;
        
        private static bool _alreadyShowWin = false;
        private static bool _initialized = false;
        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            _quests = Resources.LoadAll<Quest>("").ToList();
            Debug.Log(_quests.Count);
        }

        public static void StartNewGame()
        {
            foreach (var quest in _quests)
            {
                quest.alreadyCompleted = false;
            }
        }
        public static void SaveQuests()
        {
            QuestSaveData saveData = new QuestSaveData();

            foreach (var quest in _quests)
            {
                saveData.quests.Add(new SerializableQuestData(quest));
            }

            PermanentData.Save(saveData, nameof(QuestsManager));
        }

        public static void LoadQuests()
        {
            QuestSaveData saveData = PermanentData.Load(new QuestSaveData(), nameof(QuestsManager));

            foreach (var savedQuest in saveData.quests)
            {
                Quest quest = _quests.FirstOrDefault(q => q.name == savedQuest.questName);
                if (quest != null)
                {
                    quest.alreadyCompleted = savedQuest.alreadyCompleted;
                }
            }
        }
        
        public static bool HasLoadData
        {
            get
            {
                LoadQuests();
                return _quests.Exists(quest => quest.alreadyCompleted);
            }
        }
        
        public static bool StartNewQuest(Quest quest)
        {
            if (_activeQuest != null) return false;
            _activeQuest = quest;
            _activeQuest.UpdateTexts = null;
            Started?.Invoke(quest);
            return true;
        }

        public static void FinishQuest()
        {
            Finished?.Invoke();
            if (Victory()&& !_alreadyShowWin)
            {
                Win?.Invoke();
                _alreadyShowWin = true;
            }
            _activeQuest = null;
        }
        private static bool Victory() => _quests.All(quest => quest.alreadyCompleted);
        
    }
    [Serializable]
    public class SerializableQuestData
    {
        public string questName;
        public bool alreadyCompleted;

        public SerializableQuestData(Quest quest)
        {
            questName = quest.name;
            alreadyCompleted = quest.alreadyCompleted;
        }
    }

    [Serializable]
    public class QuestSaveData
    {
        public List<SerializableQuestData> quests = new List<SerializableQuestData>();
    }
}
