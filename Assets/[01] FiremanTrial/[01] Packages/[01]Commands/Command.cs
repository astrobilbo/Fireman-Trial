using System;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public class Command : MonoBehaviour
    {
        public Action ExecuteAction;

        public virtual void Execute()
        {
            Debug.Log($"commandID: {nameof(Command)} executed at {Time.time}");
            ExecuteAction?.Invoke();
        }
        
    }
}
