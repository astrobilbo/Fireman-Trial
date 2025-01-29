using UnityEngine;
using FiremanTrial.Movement;

namespace FiremanTrial.Commands
{
    public class RotateMovementCommand : AxesCommand
    {
        [SerializeField] private MovementRotation movement;
        
        public override void Execute(float value) => movement.HandleRotationInput(value);
    }
}
