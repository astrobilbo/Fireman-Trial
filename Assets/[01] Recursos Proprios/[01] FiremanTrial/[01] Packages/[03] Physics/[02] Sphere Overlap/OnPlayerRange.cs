using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.PhysicsInteraction
{
    public class OnPlayerRange : SphereOverlap
    {
        [SerializeField] UnityEvent OnPlayerRangeHit;
        [SerializeField] private bool callOncePerEntry;
        private bool playerInRange;
        private Collider _collider;
        private bool _called;

        protected override void BeforeLoopInteractions()
        {
            FindTargetObject = false;
        }

        protected override bool FindTargetObjectsInRange(int i)
        {
                _collider = HitColliders[i];
                return true;
        }

        protected override void AfterLoopInteractions()
        {
            ExecuteInteractions();
            EndOldInteractions();
        }
        
        private  void ExecuteInteractions()
        {
            if (FindTargetObject && (!callOncePerEntry || !_called) )
            {
                OnObjectPositionRange();
                _called = true;
                OnPlayerRangeHit?.Invoke();
            }
        }

        private  void EndOldInteractions()
        {
            if (FindTargetObject || !_collider) return;
            _called = false;
            _collider = null;
        }
    }
}