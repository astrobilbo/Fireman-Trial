using FiremanTrial.Commands;
using UnityEngine;
using VirtualJoystick;

namespace FiremanTrial.JoystickConverter
{
    public class ConvertJoystickToAxes : MonoBehaviour
    {
        [SerializeField] private Joystick variableJoystick;
        [SerializeField] private AxesCommand verticalFloatCommand;
        [SerializeField] private AxesCommand horizontalFloatCommand;
        [SerializeField] private bool updateOnlyIfDirectionChange;
        private Vector2 _lastDirection = Vector3.zero;

        private void Awake() => _lastDirection = variableJoystick.Direction;

        private void OnEnable() => variableJoystick.DirectionChanged += UpdateAxes;

        private void OnDisable() => variableJoystick.DirectionChanged -= UpdateAxes;

        private void UpdateAxes(Vector2 joystickDirection)
        {
            if (updateOnlyIfDirectionChange && joystickDirection == _lastDirection) return;
            verticalFloatCommand.Execute(joystickDirection.y);
            horizontalFloatCommand.Execute(joystickDirection.x);
            _lastDirection = joystickDirection;
        }
    }
}