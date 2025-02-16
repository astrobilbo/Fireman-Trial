using FiremanTrial.Fire;
using FiremanTrial.InteraciveObjects;
using UnityEngine;

namespace FiremanTrial.EnergyBox
{
    public class EnergyBox : MonoBehaviour
    {
        [SerializeField] private FireManager fire;
        [SerializeField] private int fireBurnValue;
        [SerializeField] private int fireStopBurnValue;
        [SerializeField] private InteractiveObject interactiveObject;
        private bool isOn = false;
        private void OnEnable() => SetObserver();
        private void OnDisable() => RemoveObserver();

        private void SetObserver()
        {
            if (interactiveObject is null) return;
            interactiveObject.StartInteractionActions += TriggerButton;
            interactiveObject.StartBoolInteractionActions += Button;
        }
        
        private void RemoveObserver()
        {
            if (interactiveObject is null) return;
            interactiveObject.StartInteractionActions -= TriggerButton;
            interactiveObject.StartBoolInteractionActions -= Button;
        }
        
        private void Button(bool open)
        {
            if(open) On();
            else Off();
        }
        
        private void TriggerButton()
        {
            if (isOn)
                Off();
            else
                On();
        }
        
        public void On()
        {
            if (isOn) return;
            isOn = true;
            if (!fire) return;
            fire.IncreaseFireRate(fireBurnValue + fireStopBurnValue);
        }

        public void Off()
        {
            if (!isOn) return;
            isOn = false;
            if (fire) fire.ReduceFireRate(fireBurnValue+ fireStopBurnValue);
        }
    }
}
