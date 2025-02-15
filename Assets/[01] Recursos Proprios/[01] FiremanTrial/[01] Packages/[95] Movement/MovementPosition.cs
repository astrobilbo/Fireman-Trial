using System;
using System.Collections.Generic;
using FiremanTrial.Manager;
using UnityEngine;

namespace FiremanTrial.Movement
{
    public class MovementPosition : MonoBehaviour
    {
        public event Action<Vector3> DirectionObserver;        
        public event Action<bool> BooleanObserver;
        
        [SerializeField] private float walkingspeed = 1;
        [SerializeField] private float runningspeed = 1.2f;
        
        private CharacterController _characterController;
        private Vector3 _desiredDirection = Vector3.zero;
        
        private bool _movementActive;
        private bool _canMove = true;
        private bool _running = false;
        
        private List<MovementDirection> _movingDirections = new List<MovementDirection>();
        
        private void Awake() => _characterController = GetComponent<CharacterController>();

        private void FixedUpdate() => ApplyMovement();
        
        private void OnEnable() => GameManager.GameStateChanged += MovementReactionToGameStateChange;

        private void OnDisable() => GameManager.GameStateChanged -= MovementReactionToGameStateChange;

        //para o movimento se o estado do jogo nao for jogando
        private void MovementReactionToGameStateChange(GameState gameState)
        {
            if (gameState == GameState.Playing)
            {
                RestartMovement();
            }
            else
            {
                StopMovement();
            }
        }
        
        public void Run() => _running = !_running;
        private float Speed => _running ? runningspeed : walkingspeed;
        

        public void AddMovementInput(MovementDirection direction)
        {
            if (!_canMove) return;
            if (_movingDirections.Contains(direction)) return;
            _movementActive = true;
            BooleanObserver?.Invoke(_movementActive);
            _desiredDirection += TranslateDirectionToVector(direction);
            _movingDirections.Add(direction);
        }

        public void RemoveMovementInput(MovementDirection direction)
        {
            if (!_canMove) return;
            if (!_movingDirections.Contains(direction)) return;
            _desiredDirection -= TranslateDirectionToVector(direction);
            _movingDirections.Remove(direction);
        }

        private Vector3 TranslateDirectionToVector(MovementDirection direction) =>
            direction switch
            {
                MovementDirection.Forward => Vector3.forward,
                MovementDirection.Backward => Vector3.back,
                MovementDirection.Left => Vector3.left,
                MovementDirection.Right => Vector3.right,
                _ => Vector3.zero,
            };

        private void ApplyMovement()
        {
            if (!_canMove) return;
            if (!_movementActive) return;
            
            var isStationary = CheckIfStationary();
            if (isStationary) return;

            var moveDirection = ComputeMovementVector();
            DirectionObserver?.Invoke(_desiredDirection);
            
            _characterController.Move(moveDirection);
        }
        
        private bool CheckIfStationary()
        {
            _movementActive = !(_desiredDirection == Vector3.zero &&
                                _characterController.velocity == Vector3.zero);
                if (!_movementActive)
                {
                    DirectionObserver?.Invoke(_desiredDirection);
                    BooleanObserver?.Invoke(_movementActive);
                }
                return !_movementActive;
        }

        private Vector3 ComputeMovementVector()
        {
            var verticalDirection = _characterController.transform.TransformDirection(Vector3.forward);
            var horizontalDirection = _characterController.transform.TransformDirection(Vector3.right);
            return CalculateMovement(horizontalDirection, verticalDirection) ;
        }
        
        private Vector3 CalculateMovement(Vector3 horizontal, Vector3 vertical)
        {
            var direction = (_desiredDirection.x * horizontal + _desiredDirection.z * vertical).normalized;
            var speedInFrame = (Speed * Time.fixedDeltaTime);
            var result =   direction * speedInFrame;
            return result;
        }

        public void StopMovement()
        {
            if (!_canMove) return;
            if (_movingDirections is null || _movingDirections.Count == 0) return;
            var directions=new List<MovementDirection>(_movingDirections);
            foreach (var activeMovingDirecion in directions)
            {
                RemoveMovementInput(activeMovingDirecion);
            }

            ApplyMovement();
            _canMove = false;
        }

        public void RestartMovement()
        {
            _canMove = true;
        }
    }
    public enum MovementDirection
    {
        Forward,
        Backward,
        Left,
        Right
    }
}
