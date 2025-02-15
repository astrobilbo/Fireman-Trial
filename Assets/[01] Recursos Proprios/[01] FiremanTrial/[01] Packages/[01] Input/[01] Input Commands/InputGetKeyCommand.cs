using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.InputManager
{
    public class InputGetKeyCommand : InputGetKeyBase
    {
        [SerializeField] private Command keyDown;
        [SerializeField] private Command keyPressed;
        [SerializeField] private Command keyUp;

        protected override void InputKeyDown()
        {
            if (keyDown != null && keyDown.CanExecute()) base.InputKeyDown();
        }

        protected override void InputKeyPressed()
        {
            if (keyPressed != null && keyPressed.CanExecute()) base.InputKeyPressed();
        }

        protected override void InputKeyUp()
        {
            if (keyUp != null && keyUp.CanExecute()) base.InputKeyUp();
        }

        protected override void KeyDown() => keyDown?.Execute();

        protected override void KeyPressed() => keyPressed?.Execute();

        protected override void KeyUp() => keyUp?.Execute();
    }
}