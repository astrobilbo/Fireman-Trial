using FiremanTrial.InteraciveObjects;
using UnityEngine;

namespace FiremanTrial.Inventory
{
    public abstract class InventoryItem : MonoBehaviour
    {
        [SerializeField] protected InteractiveObject _interactiveObject;
        protected InventoryManager inventoryManager;
        protected bool _inInventory;
        private Transform placeHolder;
        private Quaternion startRotation;

        private void Start()
        {
            inventoryManager=FindAnyObjectByType<InventoryManager>();
            placeHolder = transform.parent;
            startRotation = transform.rotation;
        }

        private void OnEnable() => SetObserver();
        private void OnDisable() => RemoveObserver();

        private void SetObserver() => _interactiveObject.StartInteractionActions += OnPickUp;

        private void RemoveObserver() => _interactiveObject.StartInteractionActions -= OnPickUp;

        protected virtual void OnPickUp()
        {
            _inInventory = true;
            _interactiveObject.ExternalInteractionLock(true);
            inventoryManager.PickUpItem(this);
        }

        public virtual void ReturnToInitialPosition()
        {
            transform.transform.parent = (placeHolder);
            transform.position = Vector3.zero;
            transform.localPosition = Vector3.zero;
            transform.rotation = startRotation;
            _inInventory = false;
            _interactiveObject.ExternalInteractionLock(false);
        }

        public virtual void RemoveItem()
        {
            _inInventory = false;
            _interactiveObject.ExternalInteractionLock(false);
        }
    }
}