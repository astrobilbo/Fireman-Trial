using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace FiremanTrial.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class Gravity : MonoBehaviour
    {
        private CharacterController _characterController;
        private Vector3 _gravityVelocity=Vector3.zero;
        private  float _gravityForce = Physics.gravity.y;
        private  float _fallMultiplier = 2f;
        private float _groundPreventSticking = -2f;
        private void Awake() => _characterController = GetComponent<CharacterController>();

        private void FixedUpdate() => ApplyGravity();

        private void ApplyGravity()
        {
            if (_characterController.isGrounded && _gravityVelocity.y < 0)
            {
                _gravityVelocity.y = _groundPreventSticking;
            }
            else
            {
                _gravityVelocity.y += _gravityForce * _fallMultiplier * Time.fixedDeltaTime;
            }
            _characterController.Move(_gravityVelocity * Time.fixedDeltaTime);

        }
        
    }
}
