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
        private float updateTime;
        private float fireTimer;
        private bool playing;
        private bool _isInteracting;

        private void Update()
        {
            if (!playing)return;
            fireTimer += Time.deltaTime;
            updateTime = RateToWin()? winUpdateTime : timeToUpdateFire;
            if (updateTime > fireTimer) return;
            fireTimer = 0f;
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
            Debug.Log("reduced by: "+level);
            currentFireRate -= level;
            fireLevelChanged?.Invoke();
            if (RateToWin() && GetTimeToWin()>0)
            {
                updateTime = winUpdateTime;
            }
            else if (!RateToWin() && GetTimeToFail()>0)
            {
                updateTime = timeToUpdateFire;
            }
            else
            {
                UpdateFire();
            }
        }
        public void IncreaseFireRate (int level)
        {
            Debug.Log("increase by: "+level);
            currentFireRate += level;
            fireLevelChanged?.Invoke();
            updateTime = RateToWin()? winUpdateTime : timeToUpdateFire;
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
            if (!playing)return;
            fireParticles.Stop();
            playing = false;
            FireExtinguisherSuccess?.Invoke();
        }


        public void Fail()
        {
            if (!playing)return;
            fireParticles.Stop();
            playing=false;
            FireExtinguisherFail?.Invoke();
        }

        public float GetTimeToFail()
        {
            var value = (maxLevel - currentLevel) / (float)currentFireRate * timeToUpdateFire;
            if (value < 0)
            {
                value = 0;
            }

            return value;
        }

        public float GetTimeToWin()
        {
            var value=(currentLevel) / Mathf.Abs(currentFireRate) * winUpdateTime;
            if (value<0)
            {
                value=0;
            }

            return value;
        }

        public bool RateToWin()
        {
            return currentFireRate < 0;
        }
    }
}