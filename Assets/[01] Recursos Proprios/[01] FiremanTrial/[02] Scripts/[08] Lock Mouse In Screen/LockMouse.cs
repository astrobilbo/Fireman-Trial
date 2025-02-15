
using UnityEngine;

namespace FiremanTrial
{
    public class LockMouse : MonoBehaviour
    {
        private bool lockState;

        private void Awake()
        {
            lockState = Cursor.visible;
        }

        public void Lock(bool value)
        {
            lockState = value;
            UpdateCursorState();
        }

        public void Toggle()
        {
            lockState=!lockState;
            UpdateCursorState();
        }

        private void UpdateCursorState()
        {
            Cursor.lockState = lockState ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !lockState;
        }
    }
}
