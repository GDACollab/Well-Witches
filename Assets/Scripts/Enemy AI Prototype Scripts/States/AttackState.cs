using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Unity.VisualScripting;

public class AttackState : State
{
    [Header("Attack Settings")]
    private StateMachine stateMachine;
    private BaseEnemyClass enemy;
    private NavMeshAgent agent;

    [Header("Debug")]
    [SerializeField] private float attackTime;


    public AttackState(GameObject owner, GameObject player) : base(owner) { }
    public void Initialize(StateMachine stateMachine, GameObject owner, Transform target)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        enemy = owner.GetComponent<BaseEnemyClass>();
        agent = owner.GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        attackTime = Time.time - enemy.timeBetweenAttack;
        agent.speed = 0f;
    }

    public override void OnUpdate()
    {
        if (enemy && !enemy.isStunned)
        {
            if (Time.time >= attackTime + enemy.timeBetweenAttack)
            {
                enemy.Attack();
                attackTime = Time.time;
            }
        }
    }

    public override void OnExit()
    {
        agent.speed = enemy.moveSpeed;
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new StunnedTransition(stateMachine, owner),
            new OutotRangeTransition(stateMachine, owner),
        };
    }
}