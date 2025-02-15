using FiremanTrial.Fire;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.EnergyBox
{
    public class EnergyBox : MonoBehaviour
    {
        [SerializeField] private FireManager fire;
        [SerializeField] private int fireBurnValue;
        [SerializeField] private int fireStopBurnValue;
        public void On()
        {
            if (!fire) return;
            if (fire.Initialize()) fire.IncreaseFireRate(fireBurnValue);
            else fire.IncreaseFireRate(fireBurnValue + fireStopBurnValue);
        }

        public void Off()
        {
            if (fire) fire.ReduceFireRate(fireBurnValue+ fireStopBurnValue);
        }
    }
}
