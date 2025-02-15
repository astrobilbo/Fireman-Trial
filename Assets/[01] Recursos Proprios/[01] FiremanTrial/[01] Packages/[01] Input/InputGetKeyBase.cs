using UnityEngine;

namespace FiremanTrial.InputManager
{
    public abstract class InputGetKeyBase : MonoBehaviour
    {
        [SerializeField] private KeyCode key;

        void Update()
        {
            InputKeyDown();
            InputKeyPressed();
            InputKeyUp();
        }

        protected virtual void InputKeyDown()
        {
            if (Input.GetKeyDown(key)) KeyDown();
        }

        protected virtual void InputKeyPressed()
        {
            if (Input.GetKey(key)) KeyPressed();
        }

        protected virtual void InputKeyUp()
        {
            if (Input.GetKeyUp(key)) KeyUp();
        }
        
        protected abstract void KeyDown();
        protected abstract void KeyPressed();
        protected abstract void KeyUp();

    }
}
