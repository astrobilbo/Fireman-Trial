using FiremanTrial.Inventory;
using UnityEngine;

namespace FiremanTrial.Extinguisher
{
    public class FireExtinguisher : InventoryItem
    {
        
        [SerializeField] private ParticleSystem _contentParticleSystem;
        [SerializeField] AudioSource _audioSource;
        
        private const float LoseCapacityPerSecond = 0.01f;
        private const float TimeBetweenCheck = 1f;
        private const float InteractionCooldownSeconds = 2f;
    
        private bool active = false;
        private float interactionTimer = 0f;
        private float _capacity = 100;
        private float _timeSinceLastCheck = 1f;
        private void Update()
        {
            if(!_inInventory)return;
            
        }

        public string Name() => name;
    }
}