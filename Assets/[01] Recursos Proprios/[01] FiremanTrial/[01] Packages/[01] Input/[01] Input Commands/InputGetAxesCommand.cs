using FiremanTrial.Commands;
using UnityEngine;

namespace FiremanTrial.InputManager
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
