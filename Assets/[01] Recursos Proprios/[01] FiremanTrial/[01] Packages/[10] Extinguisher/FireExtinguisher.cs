using FiremanTrial.Inventory;
using FiremanTrial.PhysicsInteraction;
using UnityEngine;

namespace FiremanTrial.Extinguisher
{
    public class FireExtinguisher : InventoryItem
    {
        
        [SerializeField] private ParticleSystem _ps;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] Raycast _raycast;
        
        public override void ReturnToInitialPosition()
        {
            base.ReturnToInitialPosition();
            DesactiveExtinguisher();
        }
        
        public void ActiveExtinguisher()
        {
            if(!_inInventory)return;
            _raycast.enabled = true;
            _ps.Play();
        }

        public void DesactiveExtinguisher()
        {
            _raycast.EndCurrentInteraction();
            _raycast.enabled = false;
            _ps.Stop();
        }
        
    }
}