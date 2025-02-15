using FiremanTrial.Manager;
using UnityEngine;

namespace FiremanTrial.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementRotation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 10;
        
        private CharacterController _characterController;
        private bool _canMove = true;
        private void OnEnable() => GameManager.GameStateChanged += MovementReactionToGameStateChange;

        private void OnDisable() => GameManager.GameStateChanged -= MovementReactionToGameStateChange;

        //para o movimento se o estado do jogo nao for jogando
        private void MovementReactionToGameStateChange(GameState gameState) => _canMove = gameState == GameState.Playing;
        private void Awake() => _characterController = GetComponent<CharacterController>();

        public void ChangeRotationSpeed(float speed) => rotationSpeed = speed;

        public void HandleRotationInput(float direction) => RotateCharacter(direction);

        private void RotateCharacter(float direction)
        {
            if (!_canMove) return;
            _characterController.transform.Rotate(Vector3.up, UpdateRotationAngle(direction));
        }
        
        private float UpdateRotationAngle(float direction) => direction * rotationSpeed * Time.fixedDeltaTime;

    }
}
