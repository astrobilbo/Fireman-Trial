using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FiremanTrial.Quests
{
    public static class QuestsManager 
    {
        public static Action<Quest> Started;
        public static Action Finished;
        
        private static List<Quest> _quests= new List<Quest>();
        private static Quest _activeQuest;
        
        private static bool Victory() => _quests.All(quest => quest.AlreadyCompleted);
        
        public static bool AddQuest(Quest quest)
        {
            if (_quests.Contains(quest))
            {
                return false;
            }
            _quests.Add(quest);
            return true;
        }

        public static void RemoveQuest(Quest quest)
        {
            if (_quests.Contains(quest))
            {
                _quests.Remove(quest);
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
            if (Victory())
            {
                Debug.Log("Ganhou o jogo");
            }
            _activeQuest = null;
        }
    }
}
