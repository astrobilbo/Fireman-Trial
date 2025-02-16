using System;
using UnityEngine;

namespace FiremanTrial.Fire
{
    public class FireManager : MonoBehaviour
    {
        public Action<FireManager> FireStarted;
        public Action fireLevelChanged; 
        public Action FireExtinguisherFail;
        public Action FireExtinguisherSuccess;
        [SerializeField] private int startLevel;
        [SerializeField] private int maxLevel;
        [SerializeField] private int fireRate;
        [SerializeField] private float timeToUpdateFire;
        [SerializeField] private ParticleSystem fireParticles;
        [SerializeField] private float particleRateMultiplier=1;
        [SerializeField] private float winUpdateTime= 10f;

        private int currentLevel;
        private int currentFireRate;
        private float fireTimer;
        private bool playing;
        private bool _isInteracting;
        private bool _inIntercation;

        private void Update()
        {
            if (!playing)return;
            fireTimer += Time.deltaTime;
            var updateTime = RateToWin()? winUpdateTime : timeToUpdateFire;
            if (updateTime > fireTimer) return;
            fireTimer = 0f;
            Debug.Log(currentLevel);
            UpdateFire();
        }

        public void StartFire()
        {
            Initialize();
        }
        public bool Initialize()
        {
            if (playing) return false;
            currentLevel=startLevel;
            currentFireRate=fireRate;
            fireParticles.Play();
            var emission = fireParticles.emission;
            emission.rateOverTime = currentLevel;
            playing = true;
            FireStarted?.Invoke(this);
            return true;
        }
        
        public void ReduceFireRate (int level)
        {
            Debug.Log("reduce "+level + " to " +currentLevel);
            currentFireRate -= level;
            fireLevelChanged?.Invoke();
        }
        public void IncreaseFireRate (int level)
        {
            Debug.Log("increase "+ level + " to " +currentLevel);
            currentFireRate += level;
            fireLevelChanged?.Invoke();
        }

        
        private void UpdateFire()
        {
            currentLevel += currentFireRate;
            if (currentLevel >= maxLevel)
            {
                Fail();
                return;
            }

            if (currentLevel <= 0)
            {
                Win();
                return;
            }
            var emission = fireParticles.emission;
            emission.rateOverTime = currentLevel*particleRateMultiplier;
        }

        private void Win()
        {
            fireParticles.Stop();
            Debug.Log("Win");
            playing = false;
            FireExtinguisherSuccess?.Invoke();
        }


        public void Fail()
        {
            if (!playing)return;
            fireParticles.Stop();
            Debug.Log("Fail");
            playing=false;
            FireExtinguisherFail?.Invoke();
        }

        public float GetTimeToFail()
        {
            return maxLevel*timeToUpdateFire-currentLevel*timeToUpdateFire;
        }
        public float GetTimeToWin()
        {
            return currentLevel*winUpdateTime;
        }

        public bool RateToWin()
        {
            return currentFireRate < 0;
        }
    }
}