using System;
using FiremanTrial.Inventory;
using FiremanTrial.PhysicsInteraction;
using UnityEngine;

namespace FiremanTrial.Extinguisher
{
    public class FireExtinguisher : InventoryItem
    {
        public Action InHand;
        public Action OutHand;
        [SerializeField] private ParticleSystem _ps;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] Raycast _raycast;

        public override void OnPickUp()
        {
            base.OnPickUp();
            InHand ?.Invoke();
        }
        public override void ReturnToInitialPosition()
        {
            base.ReturnToInitialPosition();
            DesactiveExtinguisher();
            OutHand?.Invoke();
        }
        
        public void ActiveExtinguisher()
        {
            if(!_inInventory)return;
            _raycast.enabled = true;
            _ps.Play();
        }

        public void DesactiveExtinguisher()
        {
            if(!_inInventory)return;
            _raycast.EndCurrentInteraction();
            _raycast.enabled = false;
            _ps.Stop();
        }
        
    }
}