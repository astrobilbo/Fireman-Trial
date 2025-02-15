using System;
using System.Collections;
using UnityEngine;

namespace FiremanTrial.ScriptAnimator
{
    public class RotateTransform : MonoBehaviour
    {
        [SerializeField] private float movementSpeed=50f;        
        [SerializeField] private float threshold = 0.1f; 
        private Coroutine _rotationCoroutine;
        public Action EndRotation;
        public void Rotate(Quaternion targetRotation)
        {
            if (_rotationCoroutine != null)
            {
                StopCoroutine(_rotationCoroutine);
                _rotationCoroutine = null;
            }
            _rotationCoroutine = StartCoroutine(RotateContinuously(targetRotation));
        }

        public bool CanRotate(Quaternion targetRotation) => !(Quaternion.Angle(transform.rotation, targetRotation) < 0.1f);

        private IEnumerator RotateContinuously(Quaternion targetRotation)
        {
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
                    movementSpeed * threshold);
                yield return null;
            }
            transform.rotation = targetRotation;
            EndRotation?.Invoke();
            _rotationCoroutine = null;
        }
    }
}