using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.InputCommands
{
    public abstract class InputGetKeyBase : MonoBehaviour
    {
        [SerializeField] private KeyCode key;

        void Update()
        {
            if (Input.GetKeyDown(key))
            {
                KeyDown();
            }
            if (Input.GetKey(key))
            {
                KeyPressed();
            }
            if (Input.GetKeyUp(key))
            {
                KeyUp();
            }
        }

        protected abstract void KeyDown();
        protected abstract void KeyPressed();
        protected abstract void KeyUp();
        
    }
}
