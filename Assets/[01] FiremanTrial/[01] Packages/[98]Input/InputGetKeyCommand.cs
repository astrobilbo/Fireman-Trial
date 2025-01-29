using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.InputCommands
{
    public class InputGetKeyCommand : InputGetKeyBase
    {
        [SerializeField] private Command keyDown;
        [SerializeField] private Command keyPressed;
        [SerializeField] private Command keyUp;

        protected override void KeyDown()
        {
            keyDown?.Execute();
        }

        protected override void KeyPressed()
        {
            keyPressed?.Execute();
        }

        protected override void KeyUp()
        {
            keyUp?.Execute();
        }
    }
}