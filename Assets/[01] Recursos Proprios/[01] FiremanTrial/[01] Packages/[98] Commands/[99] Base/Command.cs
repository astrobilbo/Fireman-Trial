using System;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public abstract class Command : MonoBehaviour
    {
        public virtual string CommandID { get;}
        public Action ExecuteAction;

        public virtual void Execute()
        {
            ExecuteAction?.Invoke();
        }

        public virtual bool CanExecute(){return true;}
    }
}
