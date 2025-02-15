using LendasEMitos.Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.DialogueOverride
{
    public class GetPlayerNameForDialogue : MonoBehaviour
    {
        [SerializeField] private string defaultPlayerName;
        [SerializeField] private TMP_InputField playerNameInputField;
        [SerializeField] private Button button;
        private string _playerName;
        private Conversant _conversant;       
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _conversant = FindAnyObjectByType<Conversant>();
            CanvasGroupManager.VisibleAndInteractive(true, _canvasGroup);
            if (playerNameInputField.placeholder is TextMeshProUGUI placeholderText)
            {
                placeholderText.text = $"Default name: {defaultPlayerName}";
            }
            _playerName = defaultPlayerName;
            playerNameInputField.onValueChanged.AddListener(SetPlayerName);
            button.onClick.AddListener(ChangePlayerName);
        }

        private void SetPlayerName(string newPlayerName)
        {
            _playerName = newPlayerName;
        }

        private void ChangePlayerName()
        {
            _conversant?.ChangePlayerName(!string.IsNullOrEmpty(_playerName) ? _playerName : defaultPlayerName);
            CanvasGroupManager.VisibleAndInteractive(false, _canvasGroup);
        }
    }
}
