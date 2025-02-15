using FiremanTrial.InteraciveObjects;
using UnityEngine;

namespace FiremanTrial.PhysicsInteraction
{
    public class Raycast : MonoBehaviour
    {
        [SerializeField] private float maxDistanceToTakeObjects = 1f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Vector3 targetPoint = new Vector3(0.5f, 0.5f, 0);
        private Camera _camera;
        private RaycastHit[] _raycastResults = new RaycastHit[10];
        private InteractiveObject _activeInteractiveObject;

        private void Awake() => InitializeDependencies();

        private void FixedUpdate() => PerformRaycastAndUpdateInteractions();

        private void InitializeDependencies()
        {
            _camera = Camera.main;
            _raycastResults = new RaycastHit[10];
        }

        private void PerformRaycastAndUpdateInteractions()
        {
            var ray = CreateRayFromScreenCenter();
            var hitCount = PerformRaycast(ray);
            Debug.DrawRay(ray.origin, ray.direction * maxDistanceToTakeObjects, Color.red);

            var foundInteractiveObject = TryFindInteractiveObject(hitCount);

            if (!foundInteractiveObject && _activeInteractiveObject != null) EndCurrentInteraction();
        }
        
        private Ray CreateRayFromScreenCenter()
        {
            return _camera.ViewportPointToRay(targetPoint);
        }
        
        private int PerformRaycast(Ray ray)
        {
            return Physics.RaycastNonAlloc(ray, _raycastResults, maxDistanceToTakeObjects, layerMask);
        }
        
        private bool TryFindInteractiveObject(int hitCount)
        {
            for (var index = 0; index < hitCount; index++)
            {
                var raycastHit = _raycastResults[index];

                if (IsWall(raycastHit)) break;

                if (!raycastHit.transform.TryGetComponent<InteractiveObject>(out var interactiveObject)) continue;
                
                HandleInteraction(interactiveObject);
                return true;
            }

            return false;
        }
        
        private bool IsWall(RaycastHit raycastHit) => raycastHit.transform.CompareTag("Wall");

        private void HandleInteraction(InteractiveObject interactiveObject)
        {
            if (_activeInteractiveObject != null && _activeInteractiveObject.name == interactiveObject.name) return;
            
            EndCurrentInteraction();
            StartNewInteraction(interactiveObject);
        }
        
        private void EndCurrentInteraction()
        {
            _activeInteractiveObject?.EndInteraction();
            _activeInteractiveObject = null;
        }
        
        private void StartNewInteraction(InteractiveObject interactiveObject)
        {
            _activeInteractiveObject = interactiveObject;
            _activeInteractiveObject.InteractionOnView();
        }
    }
}