using System.Linq;
using FiremanTrial;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace LendasEMitos.Dialogue.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Conversant))]
    public class DialogueUI : MonoBehaviour
    {
        private Conversant _conversant;
        private CanvasGroup _canvasGroup;

        [SerializeField] private TextMeshProUGUI currentSpeaker;
        [SerializeField] private TextMeshProUGUI aiText;
        [SerializeField] private Button nextButton;
        [SerializeField] private Transform choiceRoot;
        [SerializeField] private GameObject choicePrefab;

        private void Awake()
        {
            _conversant = GetComponent<Conversant>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _conversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(() => _conversant.PassDialogue());
            UpdateUI();
        }

        private void UpdateUI()
        {
            CanvasGroupManager.VisibleAndInteractive(_conversant.IsActive(), _canvasGroup);
            if (!_conversant.IsActive()) return;

            NextActionButton(_conversant.IsChoosing());
            currentSpeaker.text = _conversant.GetCurrentSpeaker();
            if (_conversant.IsChoosing())
            {
                BuildChoiceList();
                aiText.text = "";
            }
            else
            {
                aiText.text = _conversant.GetChatText();
            }
        }

        private void NextActionButton(bool value)
        {
            nextButton.gameObject.SetActive(!value);
            choiceRoot.gameObject.SetActive(value);
        }

        private void BuildChoiceList()
        {
            var choices = _conversant.GetChoices().ToList();
            var choiceCount = choices.Count;
            var buttonCount = choiceRoot.childCount;

            for (int i = 0; i < Mathf.Min(choiceCount, buttonCount); i++)
            {
                var choiceInstance = choiceRoot.GetChild(i).gameObject;
                choiceInstance.SetActive(true);

                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choices[i].GetText();

                var button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.RemoveAllListeners();
                int choiceIndex = i; 
                button.onClick.AddListener(() =>
                {
                    _conversant.SelectChoice(choices[choiceIndex]);
                    UpdateUI();
                });
            }
            
            for (int i = choiceCount; i < buttonCount; i++)
            {
                choiceRoot.GetChild(i).gameObject.SetActive(false);
            }
            
            for (int i = buttonCount; i < choiceCount; i++)
            {
                var choiceInstance = Instantiate(choicePrefab, choiceRoot);

                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choices[i].GetText();

                var button = choiceInstance.GetComponentInChildren<Button>();
                int choiceIndex = i;
                button.onClick.AddListener(() =>
                {
                    _conversant.SelectChoice(choices[choiceIndex]);
                    UpdateUI();
                });

                /*
                 choiceRoot.DetachChildren();
                foreach (var choice in _conversant.GetChoices())
                {
                    var choiceInstance = Instantiate(choicePrefab, choiceRoot);

                    var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                    textComp.text = choice.GetText();

                    var button = choiceInstance.GetComponentInChildren<Button>();
                    button.onClick.AddListener(() =>
                    {
                        _conversant.SelectChoice(choice);
                        UpdateUI();
                    });
                    */
            }
        }
    }
}