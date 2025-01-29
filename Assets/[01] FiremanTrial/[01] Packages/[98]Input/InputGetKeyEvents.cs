using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.InputCommands
{
    public class InputGetKeyEvents : InputGetKeyBase
    {
        [SerializeField] private UnityEvent keyDown;
        [SerializeField] private UnityEvent keyPressed;
        [SerializeField] private UnityEvent keyUp;
        
        protected override void KeyDown()
        {
            keyDown?.Invoke();
        }

        protected override void KeyPressed()
        {
            keyPressed?.Invoke();
        }

        protected override void KeyUp()
        {
            keyUp?.Invoke();
        }
    }
}
