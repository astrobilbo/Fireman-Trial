using FiremanTrial.InteraciveObjects;
using UnityEngine;

namespace FiremanTrial.Commands
{
    public class InteractiveObjectCommand : Command
    {
        [SerializeField] private InteractiveObject interactiveObject;
        private CommandHistory commandHistory;
        public override string CommandID => gameObject.name + "_" + gameObject.GetInstanceID();

        private void Awake()
        {
            commandHistory = FindAnyObjectByType<CommandHistory>();
            commandHistory?.AddCommand(this);
        }

        public override void Execute()
        {
            base.Execute();
            interactiveObject.StartInteraction();
            commandHistory?.CommandExecuted(this);
        }

        public override bool CanExecute()
        {
            return interactiveObject.CanInteract();
        }
    }
}
