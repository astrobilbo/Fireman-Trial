using System;
using UnityEngine;

namespace FiremanTrial.PhysicsInteraction
{
    public abstract class SphereOverlap : MonoBehaviour
    {
        public event Action ObjectInRange;
        
        [SerializeField] protected float sphereRange=1f;        
        [SerializeField] private LayerMask layerMask;
        protected Collider[] HitColliders=new Collider[10];
        protected bool FindTargetObject;

        private void FixedUpdate() => InteractiveObjectLoop(PhysicsSphereOverlap());

        private int PhysicsSphereOverlap() => Physics.OverlapSphereNonAlloc(transform.position, sphereRange, HitColliders, layerMask);

        protected void InteractiveObjectLoop(int numColliders)
        {
            BeforeLoopInteractions();
            for (var i = 0; i < numColliders; i++)
            {
                if (HitColliders[i] is null) continue;
                if (!FindTargetObjectsInRange(i)) continue;
                FindTargetObject = true;
            }
            AfterLoopInteractions();
        }
        protected abstract void BeforeLoopInteractions();
        protected abstract bool FindTargetObjectsInRange(int i);
        protected abstract void AfterLoopInteractions();

        protected void OnObjectPositionRange() => ObjectInRange?.Invoke();
    }
}