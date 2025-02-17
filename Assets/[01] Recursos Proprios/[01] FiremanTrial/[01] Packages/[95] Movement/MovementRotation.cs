using FiremanTrial.Manager;
using UnityEngine;

namespace FiremanTrial.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementRotation : MonoBehaviour
    {
        [SerializeField] private int rotationSpeed = 10;

        private CharacterController _characterController;
        [SerializeField] private Settings.Settings settings;
        private bool _canRotate = true;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            ChangeRotationSpeed(settings.GetSensibility());
        }

        private void Awake()
        {
            MovementReactionToGameStateChange(GameManager.GetGameState());
        }

        //para o movimento se o estado do jogo nao for jogando
        private void MovementReactionToGameStateChange(GameState gameState)
        {
            if (gameState == GameState.Playing)
            {
                _canRotate = true;
            }
            else
            {
                _canRotate = false;
            }
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

        private void ChangeRotationSpeed(int speed) => rotationSpeed = speed;

        public void HandleRotationInput(float direction) => RotateCharacter(direction);

        private void RotateCharacter(float direction)
        {
            if (!_canRotate) return;
            _characterController.transform.Rotate(Vector3.up, UpdateRotationAngle(direction));
        }

        private float UpdateRotationAngle(float direction) => direction * rotationSpeed * Time.fixedDeltaTime;

    }
}