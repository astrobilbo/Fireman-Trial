using FiremanTrial.InteraciveObjects;
using UnityEngine;

namespace FiremanTrial.Inventory
{
    public abstract class InventoryItem : MonoBehaviour
    {
        [SerializeField] protected InteractiveObject _interactiveObject;
        protected InventoryManager inventoryManager;
        protected bool _inInventory;
        private Vector3 startPosition;
        private Vector3 startRotation;
        private Transform placeHolder;

        private void Awake()
        {
            inventoryManager=FindAnyObjectByType<InventoryManager>();
            startPosition = transform.position;
            startRotation = transform.localEulerAngles;
            placeHolder = transform.parent;
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
            transform.parent = placeHolder;
            transform.localEulerAngles = startRotation;
            transform.localPosition = startPosition;
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