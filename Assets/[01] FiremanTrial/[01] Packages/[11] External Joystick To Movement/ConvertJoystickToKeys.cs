using FiremanTrial.Commands;
using UnityEngine;
using VirtualJoystick;

namespace FiremanTrial.JoystickConverter
{
    public class ConvertJoystickToKeys : MonoBehaviour
    {
        [SerializeField] private Joystick variableJoystick;

        [SerializeField] private bool verticalDirection;
        [SerializeField] private bool horizontalDirection;
        [SerializeField] private Command positive;
        [SerializeField] private Command positiveToZero;
        [SerializeField] private Command negative;
        [SerializeField] private Command negativeToZero;
        private int _oldValue=0;
        
        private void OnEnable() => variableJoystick.DirectionChanged += UpdateAxes;

        private void OnDisable() => variableJoystick.DirectionChanged -= UpdateAxes;

        private void UpdateAxes(Vector2 joystickDirection)
        {
            if (verticalDirection && horizontalDirection)
            {
                Debug.Log("Only one direction bool is supported.", this);
            }

            if (verticalDirection)
            {
                Handler(joystickDirection.y);
            }
            else if (horizontalDirection)
            {
                Handler(joystickDirection.x);
            }
        }

        private void Handler(float xValue)
        {
            var value= Mathf.RoundToInt(xValue);
            if (value == _oldValue) return;
            
            switch (_oldValue)
            {
                case 0 when value == 1:
                    positive?.Execute();
                    break;
                case 0 when value == -1:
                    negative?.Execute();
                    break;
                case 1 when value == 0:
                    positiveToZero?.Execute();
                    break;
                case 1 when value == -1:
                    positiveToZero?.Execute();
                    negative?.Execute();
                    break;
                case -1 when value == 0:
                    negativeToZero?.Execute();
                    break;
                case -1 when value == 1 :
                    negativeToZero?.Execute();
                    positive?.Execute();
                    break;
            }     
            
            _oldValue= value;
        }

        
    }
}