using System.Collections.Generic;
using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.InputManager
{
    public class TryExecuteAllInteractions : MonoBehaviour
    {
        private List<InteractiveObjectCommand> _interactiveObjectCommand;
        private bool canExecute=true;
        
        private void Awake()
        {
            _interactiveObjectCommand = new List<InteractiveObjectCommand>(FindObjectsByType<InteractiveObjectCommand>(FindObjectsSortMode.None));
        }

        public void ExecuteCommands()
        {
            Debug.Log("Trying Execute Commands");
            for (var index = 0; index < _interactiveObjectCommand.Count; index++)
            {
                var command = _interactiveObjectCommand[index];
                if (command == null)
                {
                    _interactiveObjectCommand.RemoveAt(index);
                    continue;
                }

                if (!command.CanExecute())return;
                command.Execute();
                Debug.Log("Executing Commands");
            }
        }
    }
}