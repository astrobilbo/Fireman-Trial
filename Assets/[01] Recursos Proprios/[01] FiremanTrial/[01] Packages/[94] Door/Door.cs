using FiremanTrial.InteraciveObjects;
using FiremanTrial.ScriptAnimator;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.Object.Door
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private AudioClip doorSound;
        [SerializeField] private Quaternion doorOpenAngle;
        [SerializeField] private Quaternion doorCloseAngle;
        [SerializeField] private bool isOpen;
        [SerializeField] private InteractiveObject interactiveObject;
        [SerializeField] private RotateTransform rotateTransform;
        [SerializeField] private AudioSource audioSource;
        private Quaternion _targetRotation;

        private void OnEnable() => SetObserver();
        private void OnDisable() => RemoveObserver();

        private void SetObserver()
        {
            interactiveObject.StartInteractionActions += TriggerDoorMovement;
            interactiveObject.StartBoolInteractionActions += Move;
            rotateTransform.EndRotation += EndDoorMovement;
        }

        private void RemoveObserver()
        {
            interactiveObject.StartInteractionActions -= TriggerDoorMovement;
            interactiveObject.StartBoolInteractionActions -= Move;
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
            interactiveObject.ExternalInteractionLock(true);
            rotateTransform.Rotate(_targetRotation);
        }

        private void EndDoorMovement()
        {
            interactiveObject.ExternalInteractionLock(false);
        }

        private void PlayOneShotSound()
        {
            if (doorSound == null) return;
            audioSource.PlayOneShot(doorSound);
        }
    }
}