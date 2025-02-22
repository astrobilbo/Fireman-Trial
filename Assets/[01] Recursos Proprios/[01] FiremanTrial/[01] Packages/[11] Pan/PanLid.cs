using FiremanTrial.Inventory;
using UnityEngine;

namespace FiremanTrial.Pan
{
    public class PanLid : InventoryItem
    {
        [SerializeField] private Pan pan;

        public override void OnPickUp()
        {
            base.OnPickUp();
            pan.ActiveLid(this);
        }
        
        public void MoveLid(Transform target)
        {
            transform.transform.parent = (target);
            transform.position = Vector3.zero;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        
    }
}