using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AttackState : State
{
    private NavMeshAgent agent;
    private Transform player;

    [Header("Attack Settings")]
    public float movementSpeed = 3.5f;
    public float attackRange = 5f;
    private int attackCount = 0;

    public AttackState(GameObject owner, GameObject player) : base(owner)
    {
        agent = owner.GetComponent<NavMeshAgent>();
        this.player = player.transform;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Attack State");
        agent.speed = movementSpeed;
    }

    public override void OnUpdate()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(owner.transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
            if (attackCount < 2)
            {
                PerformAttack();
            }
            else
            {
                PerformSpecialAttack();
                attackCount = -1; // Reset count
            }
            attackCount++;
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Attack State");
        attackCount = 0;
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>();
    }

    private void PerformAttack()
    {
        Debug.Log("Performing regular attack");
    }

    private void PerformSpecialAttack()
    {
        Debug.Log("Performing special attack");
    }
}