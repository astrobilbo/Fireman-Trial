using System.Collections;
using UnityEngine;

namespace LendasEMitos.Dialogue
{
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] bool CallOnAwake;
        [SerializeField] Dialogue dialogue;
        [SerializeField] Conversant conversant;

        IEnumerator Start()
        {
            if (!CallOnAwake) yield break;
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
    }
}