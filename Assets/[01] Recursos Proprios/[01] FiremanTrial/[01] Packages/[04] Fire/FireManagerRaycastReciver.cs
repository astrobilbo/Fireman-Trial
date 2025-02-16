using System;
using FiremanTrial.PhysicsInteraction;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.Fire
{
    public class FireManagerRaycastReciver: MonoBehaviour, IRayCastInteractalble
    {
        [SerializeField] private FireManager fireManager;
        [SerializeField] private int reduceFlameRate;
        private bool _isInteracting = false;

        public string Name() => name;
        public void InteractionOnView()
        {
            if (_isInteracting) return;
            _isInteracting = true;
            fireManager.ReduceFireRate(reduceFlameRate);
        }

        public void EndInteraction()
        {
            if (!_isInteracting) return;
            fireManager.IncreaseFireRate(reduceFlameRate);
        }
    }
}