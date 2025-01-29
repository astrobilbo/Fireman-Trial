using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.InputCommands
{
    public class InputGetAxesCommand : MonoBehaviour
    {
        [SerializeField] string axesInputName = "AxesInput";
        [SerializeField] AxesCommand axesCommand;
        void Update()
        {
            axesCommand?.Execute(Input.GetAxis(axesInputName));
        }
    }
}
