using FiremanTrial.Manager;
using UnityEngine;

namespace FiremanTrial.Movement
{
    public class RotateCam : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 10;
        [SerializeField] private float verticalRotationLimitMax = 60;
        [SerializeField] private float verticalRotationLimitMin = -60;
        
        private float _xRotation = 0f;
        private float _yRotation = 0f;
        private Camera _camera;
        private bool _canRotate = true;
        
        private void Awake() => _camera = Camera.main;
        private void OnEnable()
        {
            GameManager.GameStateChanged += MovementReactionToGameStateChange;
        }

        private void OnDisable()
        {
            GameManager.GameStateChanged -= MovementReactionToGameStateChange;
        }

        private void MovementReactionToGameStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Playing:
                    _canRotate=true;
                    break;
                default:
                    _canRotate=false;
                    break;
            }
        }
        public void VerticalRotation(float direction) => RotateVertical(direction);
        
        public void HorizontalRotation(float direction) => RotateHorizontal(direction);

        public void ChangeRotationSpeed(float speed) => rotationSpeed = speed;
        
        private void RotateVertical(float direction)
        {
            if (!_canRotate) return;
            _xRotation-= direction * rotationSpeed * Time.deltaTime;
            _xRotation = Mathf.Clamp(_xRotation, -verticalRotationLimitMax, -verticalRotationLimitMin);
            if (_camera) _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        }
        
        private void RotateHorizontal(float direction)
        {
            if (!_canRotate) return;
            _yRotation += direction * rotationSpeed * Time.deltaTime;
            if (_camera) _camera.transform.localRotation = Quaternion.Euler(0, _yRotation, 0);
        }
    }
}
