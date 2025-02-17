using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public class CommandHistory : MonoBehaviour
    {
        private Dictionary<string, Command> _commands= new Dictionary<string, Command>();
        private const string CommandPath = "CommandHistory";
        private Data _data = new Data { commandHistory = new List<string>() };
        public void AddCommand(Command command)
        {
            if(!_commands.TryAdd(command.CommandID, command)) return;
        }

        public void CommandExecuted(Command command)
        {
            _data.commandHistory.Add(command.CommandID);
        }

        [ContextMenu("Load Commands")]
        public void LoadCommandHistory()
        {
            _data = PermanentData.Load(_data, CommandPath);
            foreach (var commandKey in _data.commandHistory)
            {
                if (!_commands.ContainsKey(commandKey)) continue;
                _commands[commandKey].Execute();
            }
        }

        [ContextMenu("Save Commands")]
        public void SaveCommandHistory()
        {
            PermanentData.Save(_data, CommandPath);
        }
        
        [ContextMenu("Clear Commands")]
        public void ClearHistory()
        {
            _data.commandHistory.Clear();
            SaveCommandHistory();
        }
        
        [Serializable]
        private struct Data
        {
            public List<string> commandHistory;
        }
    }
}