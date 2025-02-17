using UnityEngine;

namespace FiremanTrial.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private Transform holdPosition;

        private InventoryItem _currentItem;

       
        // ReSharper disable Unity.PerformanceAnalysis
        public void PickUpItem(InventoryItem newItem)
        {
            if (_currentItem)
            {
                ReturnToInitialPosition();
            }

            _currentItem = newItem;
            _currentItem.transform.parent = holdPosition;
            _currentItem.transform.position = Vector3.zero;
            _currentItem.transform.localPosition = Vector3.zero;
            _currentItem.transform.localRotation = Quaternion.identity;
        }

        public void ReturnToInitialPosition()
        {
            if (!_currentItem) return;
            
            _currentItem.ReturnToInitialPosition();
            _currentItem = null;
        }

        public void RemoveItem()
        {
            if (!_currentItem) return;
            _currentItem = null;
        }

        public InventoryItem GetCurrentItem() => _currentItem;
    }
}
