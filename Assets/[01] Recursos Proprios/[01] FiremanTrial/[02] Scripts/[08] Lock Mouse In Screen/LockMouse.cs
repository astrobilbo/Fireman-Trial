
using System;
using FiremanTrial.Manager;
using UnityEngine;

namespace FiremanTrial
{
    public class LockMouse : MonoBehaviour
    {
        private bool lockState;

        private void Awake()
        {
            Cursor.lockState=CursorLockMode.None;
                
        }

        public void Lock(bool value)
        {
            if (Application.platform != RuntimePlatform.WindowsPlayer) return;
            Debug.Log("LockMouse Lock called");
            lockState = value;
            UpdateCursorState();
        }

        public void Toggle()
        {
            if (Application.platform != RuntimePlatform.WindowsPlayer) return;
            Debug.Log("LockMouse Toggle called");
            Lock(!lockState);
        }

        private void UpdateCursorState()
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                Cursor.lockState = lockState ? CursorLockMode.Locked : CursorLockMode.None;
                Cursor.visible = !lockState;
                GameManager.SetGameState(lockState ? GameState.Playing : GameState.Pause);
            }
        }
    }
}
