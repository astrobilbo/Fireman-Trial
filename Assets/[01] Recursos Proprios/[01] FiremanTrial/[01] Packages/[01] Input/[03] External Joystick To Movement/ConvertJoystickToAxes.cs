using System.Collections;
using FiremanTrial.Commands;
using UnityEngine;
using VirtualJoystick;

namespace FiremanTrial.InputManager
{
    public class ConvertJoystickToAxes : MonoBehaviour
    {
        [SerializeField] private Joystick variableJoystick;
        [SerializeField] private AxesCommand verticalFloatCommand;
        [SerializeField] private AxesCommand horizontalFloatCommand;
        [SerializeField] private bool updateOnlyIfDirectionChange;
        [SerializeField] private float speedUp=1f;
        private Vector2 _lastDirection = Vector3.zero;
        private Coroutine _movementCoroutine;

        private void Awake() => _lastDirection = variableJoystick.Direction;

        private void OnEnable() => variableJoystick.DirectionChanged += UpdateAxes;

        private void OnDisable() => variableJoystick.DirectionChanged -= UpdateAxes;

        private void UpdateAxes(Vector2 joystickDirection)
        {
            if (updateOnlyIfDirectionChange)
            {
                if (joystickDirection == _lastDirection) return;
                verticalFloatCommand.Execute(joystickDirection.y);
                horizontalFloatCommand.Execute(joystickDirection.x);
                _lastDirection = joystickDirection;
                return;
            }
            
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
                _movementCoroutine = null;
            }
            _movementCoroutine = StartCoroutine(Move(joystickDirection));
        }

        private IEnumerator Move(Vector2 joystickDirection)
        {
            while (true)
            {
                verticalFloatCommand.Execute(joystickDirection.y * speedUp);
                horizontalFloatCommand.Execute(joystickDirection.x * speedUp);
                if (joystickDirection == Vector2.zero) yield break;
                yield return null;
            }
        }
    }
}