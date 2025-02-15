using System;
using UnityEngine;

namespace FiremanTrial.Quests
{
    [CreateAssetMenu(fileName = "QuestSteps", menuName = "Scriptable Objects/QuestSteps")]
    public class QuestSteps : ScriptableObject
    {
        public bool isCompleted;
        public bool active;
        public string stepObjective;
        
        public Action Started;
        public Action Failed;
        public Action Completed;    
        
        public void Start()
        {
            active = true;
            Started?.Invoke();
        }

        public void Complete()
        {
            if (!active) return;
            
            active = false;
            isCompleted = true;
            Completed?.Invoke();
        }

        public void Fail()
        {
            if (!active) return;
            if (isCompleted) return;
            Failed?.Invoke();
            active = false;
        }

    }
}