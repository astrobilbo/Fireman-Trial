using System.Collections;
using FiremanTrial.InteraciveObjects;
using FiremanTrial.ScriptAnimator;
using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.Object.Cooktop
{
    public class CooktopButton : MonoBehaviour
    {
        [SerializeField]  private RotateTransform rotateTransform;
        [SerializeField] private InteractiveObject interactiveObject;
        [SerializeField] private StoveBurn stoveBurn;
        [SerializeField] private bool isOpen = false;
        [SerializeField] private Quaternion buttonOpenAngle;
        [SerializeField] private Quaternion buttonCloseAngle;
        [SerializeField] private AudioClip turnOnSound;
        [SerializeField] private AudioSource audioSource;
        
        private void OnEnable() => SetObserver();
        private void OnDisable() => RemoveObserver();

        private void SetObserver()
        {
            if (interactiveObject is null) return;
            interactiveObject.StartInteractionActions += TriggerButton;
            interactiveObject.StartBoolInteractionActions += Button;

        }
        
        private void RemoveObserver()
        {
            if (interactiveObject is null) return;
            interactiveObject.StartInteractionActions -= TriggerButton;
            interactiveObject.StartBoolInteractionActions -= Button;
        }

        private void Button(bool open)
        {
            if(open) OpenButton();
            else CloseButton();
        }
        
        private void TriggerButton()
        {
            if (isOpen)
                CloseButton();
            else
                OpenButton();
        }
        
        private void CloseButton()
        {
            if (!isOpen) return;
            if (!rotateTransform.CanRotate(buttonCloseAngle)) return;
            isOpen = false;
            stoveBurn.Close();
            rotateTransform.Rotate(buttonCloseAngle);
        }

        private void OpenButton()
        {
            if (isOpen) return;
            if (!rotateTransform.CanRotate(buttonOpenAngle)) return;
            isOpen = true;
            PlayOneShotSound();
            stoveBurn.Open();
            rotateTransform.Rotate(buttonOpenAngle);
        }
        
        private void PlayOneShotSound()
        {
            if (turnOnSound != null && audioSource !=  null)
            {
                audioSource.PlayOneShot(turnOnSound);
            }
        }
    }
}