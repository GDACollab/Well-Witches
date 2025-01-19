using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PatrolState : State
{
    private NavMeshAgent agent;
    private Vector3 patrolTarget;
    private float idleDuration = 5f;
    private float idleTimer = 0f;
    private bool isIdling = false;

    [Header("Patrol Settings")]
    public float patrolRadius = 10f;
    public float patrolSpeed = 2f;

    public PatrolState(GameObject owner) : base(owner)
    {
        agent = owner.GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Patrol State");
        agent.speed = patrolSpeed;
        SetRandomPatrolTarget();
    }

    public override void OnUpdate()
    {
        if (isIdling)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleDuration)
            {
                isIdling = false;
                SetRandomPatrolTarget();
            }
        }
        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("Reached patrol destination, idling...");
            isIdling = true;
            idleTimer = 0f;
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Patrol State");
        isIdling = false;
        idleTimer = 0f;
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>();
    }

    private void SetRandomPatrolTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += owner.transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolTarget = hit.position;
            agent.SetDestination(patrolTarget);
        }
    }
}