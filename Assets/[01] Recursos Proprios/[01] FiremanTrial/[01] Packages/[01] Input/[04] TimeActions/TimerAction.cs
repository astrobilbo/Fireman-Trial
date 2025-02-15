using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.InputManager
{
    public class TimerAction : MonoBehaviour
    {
        [SerializeField] private UnityEvent action;
        [SerializeField] private float secondsToWait;
        [SerializeField] private bool repeat = false;
        
        public void Execute()
        {
            if (repeat) InvokeRepeating(nameof(Action), secondsToWait, secondsToWait);
            else Invoke(nameof(Action), secondsToWait);
        }

        private void Action()
        {
            action?.Invoke();
        }

        public void StopTimer()
        {
            CancelInvoke();
        }
    }
}