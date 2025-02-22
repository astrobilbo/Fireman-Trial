using System;
using System.Collections.Generic;
using System.Linq;
using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.InputManager
{
    public class TryExecuteAllInteractions : MonoBehaviour
    {
        [SerializeField]private List<InteractiveObjectCommand> interactiveObjectCommand;
        private void Start()
        {
            interactiveObjectCommand = new List<InteractiveObjectCommand>(FindObjectsByType<InteractiveObjectCommand>(FindObjectsSortMode.None));
        }

        public void ExecuteCommands()
        {
            foreach (var _interactiveObjectCommand in interactiveObjectCommand.Where(_interactiveObjectCommand => _interactiveObjectCommand.CanExecute()))
            {
                _interactiveObjectCommand.Execute();
            }
        }
    }
}