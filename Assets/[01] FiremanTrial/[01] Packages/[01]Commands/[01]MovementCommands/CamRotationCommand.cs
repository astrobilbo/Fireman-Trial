using UnityEngine;
using FiremanTrial.Movement;

namespace FiremanTrial.Commands
{
    public class CamRotationCommand : AxesCommand
    {
        [SerializeField] private RotateCam rotateCam;
        
        public override void Execute(float value) => rotateCam.VerticalRotation(value);
    }
}
