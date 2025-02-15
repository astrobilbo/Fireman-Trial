using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace LendasEMitos.Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] bool callOnAwake;
        [SerializeField] Dialogue dialogue;
        [SerializeField] Conversant conversant;
        public bool inDialogue;
        IEnumerator Start()
        {
            if (!callOnAwake) yield break;
            yield return new WaitForSeconds(1);
            ActiveConversation();
        }

        public void ActiveConversation()
        {
            if (dialogue == null)
            {
                return;
            }

            if (conversant.IsActive())
            {
                return;
            }

            conversant.StartDialogue(this, dialogue);
        }

        public void EndConversation()
        {
            inDialogue = false;
        }
    }
}