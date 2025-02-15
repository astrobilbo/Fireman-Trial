using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

namespace FiremanTrial.Fire
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private List<FireManager> _fires;
        private FireManager _fireManager;
        private float timeToFail;
        private void Awake()
        {
            _fires = new List<FireManager>(FindObjectsByType<FireManager>(FindObjectsSortMode.None));
        }

        private void OnEnable()
        {
            foreach (var fire in _fires)
            {
                fire.FireStarted += StartFire;
                fire.FireExtinguisherSuccess += EndFire;
                fire.FireExtinguisherFail += EndFire;
                fire.fireLevelChanged += Time;
            }
    
        }

        private void OnDisable()
        {
            foreach (var fire in _fires)
            {
                fire.FireStarted -= StartFire;
                fire.FireExtinguisherSuccess -= EndFire;
                fire.FireExtinguisherFail -= EndFire;
                fire.fireLevelChanged -= Time;
            }
        }

        private void StartFire(FireManager activeFire)
        {
            _fireManager = activeFire;
            Time();
            InvokeRepeating(nameof(UpdateText),0f,1f);
        }

        private void Time()
        {
            if (_fireManager.RateToWin())
            {
                timeToFail=_fireManager.GetTimeToWin();
                text.color = Color.yellow;
            }
            else
            {
                timeToFail=_fireManager.GetTimeToFail();
                text.color = Color.red;
            }
        }
        private void EndFire()
        {
            CancelInvoke();
            text.text = "";
            _fireManager = null;
        }
        
        private void UpdateText()
        {
            timeToFail--;
            var minutes = Mathf.FloorToInt(timeToFail / 60);
            var seconds = Mathf.FloorToInt(timeToFail % 60);
            text.text = minutes.ToString("00")+ ":" + seconds.ToString("00");
        }
    }
}
