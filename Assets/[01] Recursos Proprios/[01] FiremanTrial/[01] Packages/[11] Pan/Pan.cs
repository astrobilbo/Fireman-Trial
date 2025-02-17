using FiremanTrial.Fire;
using FiremanTrial.InteraciveObjects;
using FiremanTrial.Inventory;
using UnityEngine;

namespace FiremanTrial.Pan
{
    public class Pan : MonoBehaviour
    {
        [SerializeField] private Transform lidPosition;
        [SerializeField] protected InteractiveObject interactiveObject;
        [SerializeField] private int fireDecreaseRate=3;
        private FireManager _fireManager;
        private InventoryManager inventoryManager;
        private PanLid _activePanLid;
        private bool lidInPan;
        private void Start()
        {
            inventoryManager=FindAnyObjectByType<InventoryManager>();
            _fireManager=GetComponent<FireManager>();
        }

        private void OnEnable() => SetObserver();
        private void OnDisable() => RemoveObserver();

        private void SetObserver() => interactiveObject.StartInteractionActions += PlaceLidInPan;

        private void RemoveObserver() => interactiveObject.StartInteractionActions -= PlaceLidInPan;

        protected void PlaceLidInPan()
        {
            Debug.Log("Place Lid In Pan called");
            if (!_activePanLid || lidInPan) return;
            Debug.Log("Place advanced");
            _activePanLid.MoveLid(lidPosition);
            interactiveObject.EndInteraction();
            interactiveObject.ExternalInteractionLock(true);
            if (_fireManager != null)
            {
                Debug.Log(fireDecreaseRate);
                _fireManager.ReduceFireRate(fireDecreaseRate);
            }

            inventoryManager.RemoveItem();
        }

        public void ActiveLid(PanLid panLid)
        {
            _activePanLid = panLid;
            interactiveObject.ExternalInteractionLock(false);
        }

        public void RemoveLid()
        {
            if (!_activePanLid) return;
            interactiveObject.ExternalInteractionLock(true);
            _activePanLid.ReturnToInitialPosition();
            _activePanLid = null;
            lidInPan=false;
        }
    }
}