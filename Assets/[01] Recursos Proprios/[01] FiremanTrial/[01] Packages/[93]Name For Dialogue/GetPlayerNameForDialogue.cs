using LendasEMitos.Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial.DialogueOverride
{
    public class GetPlayerNameForDialogue : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private TMP_InputField playerNameInputField;
        [SerializeField] private Button button;
        [SerializeField] private PlayerDataChanger playerDataChanger;
        [SerializeField] private LockMouse lockMouse;
        private Conversant _conversant;       
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _conversant = FindAnyObjectByType<Conversant>();
             if (playerData.isLoading)
            {
                ChangePlayerName();
                CanvasGroupManager.VisibleAndInteractive(false, _canvasGroup);
                _conversant?.ChangePlayerName(playerData.playerName);
                lockMouse.Lock(true);
                return;
            }
            lockMouse.Lock(false);
            CanvasGroupManager.VisibleAndInteractive(true, _canvasGroup);
            if (playerNameInputField.placeholder is TextMeshProUGUI placeholderText)
            {
                placeholderText.text = $"Default name: {playerData.playerName}";
            }
            playerNameInputField.onValueChanged.AddListener(SetPlayerName);
            button.onClick.AddListener(ChangePlayerName);
           
        }

        private void SetPlayerName(string newPlayerName)
        {
            if (string.IsNullOrEmpty(newPlayerName)) return;
            playerData.playerName = newPlayerName;
        }

        private void ChangePlayerName()
        {
            _conversant?.ChangePlayerName(playerData.playerName);
            playerDataChanger.Save();
            CanvasGroupManager.VisibleAndInteractive(false, _canvasGroup);
        }
    }
}
