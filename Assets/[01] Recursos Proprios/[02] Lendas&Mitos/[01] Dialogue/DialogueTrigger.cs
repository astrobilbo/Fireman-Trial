using UnityEngine;
using UnityEngine.Events;

namespace LendasEMitos.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField]string action;
        [SerializeField] UnityEvent onTrigger;
        Conversant _conversant;
        AIConversant _aiConversant;

        private void Awake()
        {
            _conversant = FindAnyObjectByType<Conversant>();
            _aiConversant = GetComponent<AIConversant>();
        }

        private void OnEnable()
        {
            if (_conversant != null) _conversant.OnConversant += Trigger;
        }

        private void OnDisable()
        {
            if (_conversant.OnConversant != null) _conversant.OnConversant -= Trigger;
        }

        private void Trigger(string actionToTrigger)
        {
            if (!_aiConversant.inDialogue) return;
            if (actionToTrigger == action)
            {
                onTrigger?.Invoke();
            }
        }
    }
}