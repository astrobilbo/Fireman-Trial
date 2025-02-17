using FiremanTrial.Manager;
using UnityEngine;

namespace FiremanTrial.Movement
{
    public class RotateCam : MonoBehaviour
    {
        [SerializeField] private int rotationSpeed = 10;
        [SerializeField] private float verticalRotationLimitMax = 60;
        [SerializeField] private float verticalRotationLimitMin = -60;
        [SerializeField] private Settings.Settings settings;

        private float _xRotation = 0f;
        private float _yRotation = 0f;
        private Camera _camera;
        private bool _canRotate = true;
        
        private void Awake()
        {
            MovementReactionToGameStateChange(GameManager.GetGameState());
        }

        //para o movimento se o estado do jogo nao for jogando
        private void MovementReactionToGameStateChange(GameState gameState)
        {
            if (gameState == GameState.Playing)
            {
                _canRotate=true;
            }
            else
            {
                _canRotate=false;
            }
        }
        private void Start()
        {
            _camera = Camera.main;
            ChangeRotationSpeed(settings.GetSensibility());
        }

        private void OnEnable()
        {
            settings.OnSensibilityChanged += ChangeRotationSpeed;
            GameManager.GameStateChanged += MovementReactionToGameStateChange;
        }

        private void OnDisable()
        {
            settings.OnSensibilityChanged -= ChangeRotationSpeed;
            GameManager.GameStateChanged -= MovementReactionToGameStateChange;
        }

        public void VerticalRotation(float direction) => RotateVertical(direction);
        
        public void HorizontalRotation(float direction) => RotateHorizontal(direction);

        private void ChangeRotationSpeed(int speed) => rotationSpeed = speed;
        
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
