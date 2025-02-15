using UnityEngine;
using UnityEngine.Events;

public class MoveCharacterToTarget : MonoBehaviour
{
    [SerializeField] private AICharacters aiCharacter;
    [SerializeField] private Transform target;
    [SerializeField] private UnityEvent action;
    
    [ContextMenu("Move")]
    public void Move()
    {
        aiCharacter.MoveTo(target);
        action?.Invoke();
    }
}
