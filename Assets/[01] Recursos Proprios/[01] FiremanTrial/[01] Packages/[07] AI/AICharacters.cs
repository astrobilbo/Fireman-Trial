
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AICharacters : MonoBehaviour
{
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    [SerializeField] private Animator animator;
    [SerializeField] private string scaredAnim;
    [SerializeField] private string sillyDanceAnim;
    [SerializeField] private NavMeshAgent agent;
    
    private Coroutine _moveCoroutine;
    
    public void Scared()
    {
        animator.Play(scaredAnim,1);
    }

    public void SillyDance()
    {
        animator.Play(sillyDanceAnim,1);
    }

    public void Idle()
    {
        animator.Play("Idle",1);
    }

    public void MoveTo(Transform target)
    {
        agent.SetDestination(target.position);
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(UpdateMovement(target));
    }
    
    private IEnumerator UpdateMovement(Transform target)
    {
        animator.SetFloat(Vertical, 1);
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
            yield return null;
        }
        animator.SetFloat(Vertical, 0);
        transform.rotation = target.rotation;
    }
}
