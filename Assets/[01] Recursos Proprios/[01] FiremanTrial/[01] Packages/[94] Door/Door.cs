using FiremanTrial.InteraciveObjects;
using FiremanTrial.ScriptAnimator;
using UnityEngine;

namespace FiremanTrial.Object.Door
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private AudioClip doorSound;
        [SerializeField] private Quaternion doorOpenAngle;
        [SerializeField] private Quaternion doorCloseAngle;
        [SerializeField] private bool isOpen;
        private InteractiveObject _interactiveObject;
        private RotateTransform rotateTransform;
        private AudioSource audioSource;
        private Quaternion _targetRotation;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            rotateTransform = GetComponent<RotateTransform>();
            _interactiveObject = GetComponent<InteractiveObject>();
        }

        private void OnEnable() => SetObserver();
        private void OnDisable() => RemoveObserver();

        private void SetObserver()
        {
            _interactiveObject.StartInteractionActions += TriggerDoorMovement;
            _interactiveObject.StartBoolInteractionActions += Move;
            rotateTransform.EndRotation += EndDoorMovement;
        }

        private void RemoveObserver()
        {
            _interactiveObject.StartInteractionActions -= TriggerDoorMovement;
            _interactiveObject.StartBoolInteractionActions -= Move;
            rotateTransform.EndRotation += EndDoorMovement;
        }

        private void TriggerDoorMovement() => Move(!isOpen);
        
        private void Move(bool opening)
        {
            if (opening==isOpen)return;
            _targetRotation = opening ? doorOpenAngle : doorCloseAngle;
            if (!rotateTransform.CanRotate(_targetRotation)) return;
            isOpen = opening;
            PlayOneShotSound();
            _interactiveObject.ExternalInteractionLock(true);
            rotateTransform.Rotate(_targetRotation);
        }

        private void EndDoorMovement()
        {
            _interactiveObject.ExternalInteractionLock(false);
        }

        private void PlayOneShotSound()
        {
            if (doorSound == null) return;
            audioSource.PlayOneShot(doorSound);
        }
    }
}