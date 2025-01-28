using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LendasEMitos.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] bool isPlayerSpeaking = false;
        [SerializeField] int characterSpeaking;
        [SerializeField, TextArea(10, 20)] string text;
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] Rect rect = new Rect(0, 0, 200, 100);
        [SerializeField] string onEnterAction;
        [SerializeField] string onExitAction;
        public Rect GetRect()
        {
            return rect;
        }
        public string GetText()
        {
            return text;
        }
        public List<string> GetChildren()
        {
            return children;
        }
        public int GetCharacterSpeaking()
        {
            return characterSpeaking;
        }
        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }

        public string GetOnEnterAction()
        {
            return onEnterAction;
        }
        public string GetOnExitAction()
        {
            return onExitAction;
        }

#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "move dialogue");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);

        }
        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);

            }
        }
        public void PlayerSpeakerBool(bool newbool)
        {
            if (newbool != isPlayerSpeaking)
            {
                Undo.RecordObject(this, "update Dialogue bool");
                isPlayerSpeaking = newbool;
                EditorUtility.SetDirty(this);

            }
        }

        public void AddChildren(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);

        }
        public void RemoveChildren(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }

        public void SetCharacterSpeaking(int newCharacterSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            characterSpeaking = newCharacterSpeaking;
            EditorUtility.SetDirty(this);
        }


#endif
    }

}
