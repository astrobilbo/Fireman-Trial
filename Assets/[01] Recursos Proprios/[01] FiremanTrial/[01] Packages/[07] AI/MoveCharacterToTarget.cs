using UnityEngine;
using UnityEngine.Events;

public class MoveCharacterToTarget : MonoBehaviour
{
    [SerializeField] private AICharacters aiCharacter;
    [SerializeField] private UnityEvent action;
    
    [ContextMenu("Move")]
    public void Move()
    {
        action?.Invoke();
        aiCharacter.MoveTo(transform);
    }
}
