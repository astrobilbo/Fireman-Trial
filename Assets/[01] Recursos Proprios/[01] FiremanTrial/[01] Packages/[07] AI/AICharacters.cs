using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacters : MonoBehaviour
{
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    private Transform activeTarget;
    private Coroutine _moveCoroutine;
    private bool lookToPlayer=true;
    private Camera playerCamera;
    private Camera PlayerCamera
    {
        get
        {
            if (playerCamera == null)
                playerCamera = Camera.main;
            return playerCamera;
        }
    }
    
    private void FixedUpdate()
    {
        if (!lookToPlayer) return;
        if (PlayerCamera == null) return;
        Vector3 lookDirection = (playerCamera.transform.position - transform.position).normalized;
        lookDirection.y = 0;

        if (lookDirection == Vector3.zero) return;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    public void MoveTo(Transform target)
    {
        if (activeTarget == target) return;
        if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
        activeTarget = target;
        _moveCoroutine = StartCoroutine(UpdateMovement(target));
    }
    
    private IEnumerator UpdateMovement(Transform target)
    {
        agent.SetDestination(target.position);
        animator.SetFloat(Vertical, 1);
        lookToPlayer = false;

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            Vector3 moveDirection = agent.velocity.normalized;
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
            yield return null;
        }
        animator.SetFloat(Vertical, 0);
        lookToPlayer = true;
    }
}