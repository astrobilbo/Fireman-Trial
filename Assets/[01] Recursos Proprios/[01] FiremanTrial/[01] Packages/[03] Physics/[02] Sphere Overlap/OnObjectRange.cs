using System.Collections.Generic;
using System.Linq;

namespace FiremanTrial.PhysicsInteraction
{
    public class OnObjectRange : SphereOverlap
    {
        private List<ISphereInteractable> _activeInteractiveObject= new List<ISphereInteractable>();
        private List<ISphereInteractable> _oldInteractiveObject= new List<ISphereInteractable>();
        protected override void BeforeLoopInteractions()
        {
            FindTargetObject = false;
            _oldInteractiveObject = new List<ISphereInteractable>(_activeInteractiveObject);
            _activeInteractiveObject.Clear();
        }

        protected override bool FindTargetObjectsInRange(int i)
        {
            if (!HitColliders[i].transform.TryGetComponent<ISphereInteractable>(out var sphereInteractable)) return false;
            _activeInteractiveObject.Add(sphereInteractable);
            return true;        
        }

        protected override void AfterLoopInteractions()
        {
            ExecuteInteractions();
            EndOldInteractions();
        }

        private void ExecuteInteractions()
        {
            foreach (var sphereInteractable in _activeInteractiveObject.Where(sphereInteractable =>
                         !AlreadyCalledOnRange(sphereInteractable)))
            {
                sphereInteractable.OnRange();
            }
            OnObjectPositionRange();
        }

        private bool AlreadyCalledOnRange(ISphereInteractable sphereInteractable) => _oldInteractiveObject.Contains(sphereInteractable);

        private void EndOldInteractions()
        {
            if (OldInteractionObjectIsNullOrEmpty()) return;
            PrepareOutRangeList();
            foreach (var sphereInteractable in _oldInteractiveObject) sphereInteractable.OutRange();
            _oldInteractiveObject.Clear();
        }
        
        private void PrepareOutRangeList() =>
            _oldInteractiveObject.RemoveAll(obj => _activeInteractiveObject.Contains(obj));

        private bool OldInteractionObjectIsNullOrEmpty() =>
            _oldInteractiveObject is null || _oldInteractiveObject.Count == 0;
    }
}