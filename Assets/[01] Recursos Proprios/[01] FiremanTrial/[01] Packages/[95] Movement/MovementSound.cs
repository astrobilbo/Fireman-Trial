using UnityEngine;

namespace FiremanTrial.Movement
{
    public class MovementSound : MonoBehaviour
    {
        [SerializeField]  private AudioSource audioSource;
        [SerializeField]  private MovementPosition movingBooleanNotifier;
        [SerializeField] private AudioClip clip;
        
        public void Awake()
        {
            ChangeClip();
            StopSound();
        }

        private void OnEnable() => SetObserver();

        private void OnDisable() => RemoveObserver();

        private void SetObserver()
        {
            if (movingBooleanNotifier is null) return;
            movingBooleanNotifier.BooleanObserver += StepSound;
        }
        
        private void RemoveObserver()
        {
            if (movingBooleanNotifier is null) return;
            movingBooleanNotifier.BooleanObserver -= StepSound;
        }

        private void StepSound(bool isMoving)
        {
            if (!CanUseAudioSource()) return;

            switch (isMoving)
            {
                case true when !IsPlaying():
                    PlaySound();
                    break;
                case false when IsPlaying():
                    StopSound();
                    break;
            }
        }
        
        private void ChangeClip()
        {
            if (!CanUseAudioSource()) return;
            audioSource.clip = clip;
        }
        
        private bool CanUseAudioSource() => audioSource&& clip;

        private bool IsPlaying() => audioSource.isPlaying;

        private void PlaySound() => audioSource.Play();

        private void StopSound() => audioSource.Stop();

    }
}
