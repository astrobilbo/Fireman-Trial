using System;
using System.Collections.Generic;
using System.Linq;
using FiremanTrial.InteraciveObjects;
using UnityEngine;

namespace FiremanTrial.PhysicsInteraction
{
    public class OnObjectRange : SphereOverlap
    {
        private List<InteractiveObject> _activeInteractiveObject= new List<InteractiveObject>();
        private List<InteractiveObject> _oldInteractiveObject= new List<InteractiveObject>();
        protected override void BeforeLoopInteractions()
        {
            FindTargetObject = false;
            _oldInteractiveObject = new List<InteractiveObject>(_activeInteractiveObject);
            _activeInteractiveObject.Clear(); 
        }

        protected override bool FindTargetObjectsInRange(int i)
        {
            if (!HitColliders[i].transform.TryGetComponent<InteractiveObject>(out var interactiveObject)) return false;
            _activeInteractiveObject.Add(interactiveObject);
            return true;        
        }

        protected override void AfterLoopInteractions()
        {
            ExecuteInteractions();
            EndOldInteractions();
        }

        private void ExecuteInteractions()
        {
            foreach (var interactiveObject in _activeInteractiveObject.Where(interactiveObject =>
                         !AlreadyCalledOnRange(interactiveObject)))
            {
                interactiveObject.OnRange();
            }
            OnObjectPositionRange();
        }

        private bool AlreadyCalledOnRange(InteractiveObject interactiveObject) => _oldInteractiveObject.Contains(interactiveObject);

        private void EndOldInteractions()
        {
            if (OldInteractionObjectIsNullOrEmpty()) return;
            PrepareOutRangeList();
            foreach (var interactiveObject in _oldInteractiveObject) interactiveObject.OutRange();
            _oldInteractiveObject.Clear();
        }
        
        private void PrepareOutRangeList() =>
            _oldInteractiveObject.RemoveAll(obj => _activeInteractiveObject.Contains(obj));

        private bool OldInteractionObjectIsNullOrEmpty() =>
            _oldInteractiveObject is null || _oldInteractiveObject.Count == 0;
    }
}