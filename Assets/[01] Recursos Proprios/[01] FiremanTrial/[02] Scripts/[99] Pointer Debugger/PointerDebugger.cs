using UnityEngine;
using UnityEngine.EventSystems;

namespace FiremanTrial
{
    public class PointerDebugger : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log($"[PointerEnter] {gameObject.name}");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"[PointerDown] {gameObject.name}");
        }
    }
}