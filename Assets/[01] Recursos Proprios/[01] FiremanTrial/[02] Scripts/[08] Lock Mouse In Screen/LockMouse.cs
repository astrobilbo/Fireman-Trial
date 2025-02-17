
using FiremanTrial.Manager;
using UnityEngine;

namespace FiremanTrial
{
    public class LockMouse : MonoBehaviour
    {
        private bool lockState;

        public void Lock(bool value)
        {
            lockState = value;
            UpdateCursorState();
        }

        public void Toggle()
        {
            Lock(!lockState);
        }

        private void UpdateCursorState()
        {
            Cursor.lockState = lockState ? CursorLockMode.Locked : CursorLockMode.Confined;
            Cursor.visible = !lockState;
            GameManager.SetGameState(lockState? GameState.Playing : GameState.Pause );
        }
    }
}
