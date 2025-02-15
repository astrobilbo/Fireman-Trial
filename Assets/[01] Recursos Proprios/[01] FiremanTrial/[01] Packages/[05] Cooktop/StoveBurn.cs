using FiremanTrial.Fire;
using UnityEngine;

namespace FiremanTrial.Object.Cooktop
{
    public class StoveBurn : MonoBehaviour
    {
        [SerializeField] private ParticleSystem fire;
        [SerializeField] private FireManager panFire;
        [SerializeField] private int panBurnValue;
        [SerializeField] private int panStopBurnValue;
        public void Open()
        {
            if (fire) fire.Play();

            if (!panFire) return;
            if (panFire.Initialize()) panFire.IncreaseFireRate(panBurnValue);
            else panFire.IncreaseFireRate(panBurnValue + panStopBurnValue);
        }

        public void Close()
        {
            if (fire) fire.Stop();
            if (panFire) panFire.ReduceFireRate(panBurnValue+ panStopBurnValue);
        }
    }
}