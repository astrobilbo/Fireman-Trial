using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.InputManager
{
    public class OnStartInvoke : MonoBehaviour
    {
        [SerializeField] private UnityEvent action;
        void Start()
        {
            action?.Invoke();
        }
    }
}
