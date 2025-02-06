using UnityEngine;
using FiremanTrial.Movement;

namespace FiremanTrial.Commands
{
    public class StopMovementPositionCommand : Command
    {
        [SerializeField] private MovementPosition movement;
        [SerializeField] private MovementDirection direction;

        public override void Execute() => movement?.RemoveMovementInput(direction);
    }
}
