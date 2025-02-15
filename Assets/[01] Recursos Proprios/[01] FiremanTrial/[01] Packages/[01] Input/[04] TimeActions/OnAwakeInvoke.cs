using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.InputManager
{
    public class OnAwakeInvoke : MonoBehaviour
    {
        [SerializeField] private UnityEvent action;
        void Awake()
        {
            action?.Invoke();
        }
    }
}
