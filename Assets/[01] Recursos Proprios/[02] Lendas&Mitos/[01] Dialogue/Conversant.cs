using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LendasEMitos.Dialogue
{

    public class Conversant : MonoBehaviour
    {
        public Action<string> OnConversant;
        [SerializeField] string playerName;
        [SerializeField] string activeCharacterName;
        [SerializeField] Dialogue CurrentDialogue;
        DialogueNode currentNode = null;
        AIConversant currentConversant = null;
        bool isChoosing = false;

        public event Action onConversationUpdated;
        public void StartDialogue(AIConversant newConversant,Dialogue newDialogue)
        {
            currentConversant=newConversant;
            currentConversant.inDialogue = true;
            CurrentDialogue = newDialogue;
            currentNode = CurrentDialogue.GetRootNode();
            TriggerEnterAction();
            onConversationUpdated?.Invoke();
        }

        public void ChangePlayerName(string newPlayerName)
        {
            playerName = newPlayerName;
        }
        public void PassDialogue()
        {
            if (HasNext())
            {
                Next();
            }
            else
            {
                Quit();
            }
        }
        public void Quit()
        {
            if (!CurrentDialogue)return;
            CurrentDialogue = null;
            TriggerExitAction();
            currentNode = null;
            isChoosing = false;
            currentConversant.inDialogue = false;
            currentConversant=null;
            onConversationUpdated?.Invoke();
        }
        
        public bool IsActive()
        {
            return CurrentDialogue != null;
        }
        
        public bool IsChoosing()
        {
            return isChoosing;
        }
        
        public string GetChatText()
        {
            return currentNode != null ? currentNode.GetText() : "";
        }

        public string GetCurrentSpeaker()
        {
            if (CurrentDialogue == null) return "";
            activeCharacterName= CurrentDialogue.Characters[currentNode.GetCharacterSpeaking()];
            if (currentNode.GetCharacterSpeaking() != 0 && !isChoosing) return activeCharacterName;
            if (string.IsNullOrEmpty(playerName)) playerName=activeCharacterName;
            return playerName;
        }
        
        public IEnumerable<DialogueNode> GetChoices()
        {
            return CurrentDialogue.GetPlayerChildren(currentNode);
        }
        
        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterAction();
            isChoosing = false;
            Next();
        }
        
        public void Next()
        {
            TriggerExitAction();
            int numPlayerResponses = CurrentDialogue.GetPlayerChildren(currentNode).Count();
            GetCurrentSpeaker();
            if (numPlayerResponses > 0)
            {
                isChoosing = true;
                onConversationUpdated?.Invoke();
                return;
            }
            DialogueNode[] children = CurrentDialogue.GetAIChildren(currentNode).ToArray();
            if (children.Length > 0)
            {
                currentNode = children[UnityEngine.Random.Range(0, children.Length)];
                onConversationUpdated?.Invoke();
            }
        }

        public bool HasNext()
        {
            return CurrentDialogue.GetAllChildren(currentNode).Count() > 0;
        }

        private void TriggerEnterAction()
        {
                if (currentNode != null) TriggerAction(currentNode.GetOnEnterAction());
        }
        private void TriggerExitAction()
        {
                if (currentNode != null) TriggerAction(currentNode.GetOnExitAction());
        }
        
        private void TriggerAction(string action)
        {
            if (action=="")return;
            OnConversant?.Invoke(action);
        }
    }
}