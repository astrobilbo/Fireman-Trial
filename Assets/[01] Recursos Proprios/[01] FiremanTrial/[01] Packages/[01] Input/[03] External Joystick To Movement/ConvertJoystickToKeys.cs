using FiremanTrial.Commands;
using UnityEngine;
using VirtualJoystick;

namespace FiremanTrial.InputManager
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
        private int _oldValue = 0;

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
            var value = Mathf.RoundToInt(xValue);
            if (value == _oldValue) return;

            switch (_oldValue)
            {
                case 0 when value == 1:
                    if (positive&& positive.CanExecute()) positive.Execute();
                    break;
                case 0 when value == -1:
                    if (negative&& negative.CanExecute()) negative.Execute();
                    break;
                case 1 when value == 0:
                    if (positiveToZero&& positiveToZero.CanExecute()) positiveToZero.Execute();
                    break;
                case 1 when value == -1:
                    if (positiveToZero&& positiveToZero.CanExecute()) positiveToZero.Execute();
                    if (negative&& negative.CanExecute()) negative?.Execute();
                    break;
                case -1 when value == 0:
                    if (negativeToZero&& negativeToZero.CanExecute()) negativeToZero.Execute();
                    break;
                case -1 when value == 1:
                    if (negativeToZero&& negativeToZero.CanExecute()) negativeToZero.Execute();
                    if (positive&& positive.CanExecute()) positive.Execute();
                    break;
            }

            _oldValue = value;
        }


    }
}