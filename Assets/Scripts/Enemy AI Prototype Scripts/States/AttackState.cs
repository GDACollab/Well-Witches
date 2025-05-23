using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Unity.VisualScripting;

public class AttackState : State
{

    [Header("Attack Settings")]
    private StateMachine stateMachine;
    private BaseEnemyClass enemy;
    private Rigidbody2D rb2d;
    private NavMeshAgent agent;

    [Header("Debug")]
    [SerializeField] private float attackTime;


    public AttackState(GameObject owner, GameObject player) : base(owner) { }
    public void Initialize(StateMachine stateMachine, GameObject owner, Transform target)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        enemy = owner.GetComponent<BaseEnemyClass>();
        rb2d = owner.GetComponent<Rigidbody2D>();
        agent = owner.GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        attackTime = Time.time - enemy.timeBetweenAttack;
        attackTime = 0f;
        agent.isStopped = true;

        if (rb2d != null)
        {
            {
                rb2d.gravityScale = 0;
                agent.isStopped = true;
            }
        }
    }

    public override void OnUpdate()
    {
        if (enemy != null && !enemy.isStunned)
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
        agent.isStopped = false;
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new OutotRangeTransition(stateMachine, owner),
        };
    }
}