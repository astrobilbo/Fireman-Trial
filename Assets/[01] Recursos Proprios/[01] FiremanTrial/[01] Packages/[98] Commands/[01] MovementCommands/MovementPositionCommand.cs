using UnityEngine;
using FiremanTrial.Movement;

namespace FiremanTrial.Commands
{
    public class MovementPositionCommand : Command
    {
        [SerializeField] private MovementPosition movement;
        [SerializeField] private MovementDirection direction;

        public override void Execute() => movement?.AddMovementInput(direction);
    }
}
