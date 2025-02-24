using TMPro;
using UnityEngine;

namespace FiremanTrial.Quests.UI
{
    public class QuestUGUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI stepObjectiveText;
        [SerializeField] private TextMeshProUGUI ContextText;
        [SerializeField] private GameObject ContextTitle;
        [SerializeField] private string titleDefaultText;
        [SerializeField] private string stepObjectiveDefaultText;
        [SerializeField] private string ContextDefaultText;
        private Quest _activeQuest;

        private void Start()
        {
            UpdateTexts();
        }

        private void OnEnable()
        {
            QuestsManager.Started += QuestToFollow;
            QuestsManager.Finished += EndQuest;

        }

        private void OnDisable()
        {
            QuestsManager.Started -= QuestToFollow;
            QuestsManager.Finished -= EndQuest;
            if (_activeQuest != null) _activeQuest.UpdateTexts -= UpdateTexts;
        }

        private void QuestToFollow(Quest quest)
        {
            _activeQuest = quest;
            _activeQuest.UpdateTexts += UpdateTexts;
        }

        private void EndQuest()
        {
            if (_activeQuest != null) _activeQuest.UpdateTexts -= UpdateTexts;
            _activeQuest = null;
            UpdateTexts();
        }
        private void UpdateTexts()
        {
            
            if (_activeQuest is null)
            {
                if (titleText) titleText.text = titleDefaultText;
                if (stepObjectiveText) stepObjectiveText.text = stepObjectiveDefaultText;
                if (ContextText) ContextText.text = ContextDefaultText;
                return;
            }

            if (titleText) titleText.text = _activeQuest.uiText.title;
            if (stepObjectiveText) stepObjectiveText.text = _activeQuest.Objective();
            if (ContextText) ContextText.text = _activeQuest.uiText.info;
        }
    }
}