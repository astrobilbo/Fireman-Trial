using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
    public class Quest : ScriptableObject
    {
        public Action UpdateTexts;
        public Action Started;
        public Action Failed;
        public Action Completed;    

        public List<Quest> requiredQuests;
        public List<QuestSteps> steps;
        public UIText uiText;
        public SoundsFeedback soundsFeedback;
        private AudioSource _audioSource;

        private int _currentStep=0;
        public bool alreadyCompleted;
        public bool IsAvailable() => requiredQuests == null || requiredQuests.TrueForAll(req =>  req.alreadyCompleted);

        private bool IsCompleted() =>  steps.TrueForAll(step => step.isCompleted);

        public void Initialize(AudioSource audioSource)
        {
            _audioSource=audioSource;
        }
        public void Start()
        {
            foreach (var step in steps)
            {
                step.isCompleted = false;
                step.active = false;
            }
            _currentStep = 0;
            UpdateTexts?.Invoke();
            QuestStepSetting();
            PlayOneShotClip(soundsFeedback.startClip);
            Started?.Invoke();
        }

        private void QuestStepSetting()
        {
            var activeStep = steps[_currentStep];
            activeStep.Completed += NextStep;
            activeStep.Failed += Lose;
            activeStep.Start();
            UpdateTexts?.Invoke();
        }

        private void NextStep()
        {
            steps[_currentStep].Failed -= Lose;
            steps[_currentStep].Completed -= NextStep;

            if (IsCompleted())
            {
                Complete();
                return;
            }

            _currentStep++;
            PlayOneShotClip(soundsFeedback?.newStepClip);
            QuestStepSetting();
        }

        public void Complete()
        {
            PlayOneShotClip(soundsFeedback?.completedClip);
            alreadyCompleted = true;
            Completed?.Invoke();
            QuestsManager.SaveQuests();
            QuestsManager.FinishQuest();
        }
        
        public void Lose()
        {
            PlayOneShotClip(soundsFeedback?.loseClip);
            QuestsManager.FinishQuest();
            Failed?.Invoke();
        }

        public string Objective()
        {
            var activeStep = steps[_currentStep];
            return activeStep.stepObjective;
        }

        private void PlayOneShotClip(AudioClip clip)
        {
            if (!_audioSource || !soundsFeedback.startClip) return;
            if (_audioSource.isPlaying) return;
            _audioSource.clip=clip;
            _audioSource.Play();
        }
    }

    
    
    [Serializable]
    public class UIText
    {
        public string title;
        public string info;
    }

    [Serializable]
    public class SoundsFeedback
    {
        public AudioClip startClip;
        public AudioClip completedClip;
        public AudioClip loseClip;
        public AudioClip newStepClip;
    }
   

}