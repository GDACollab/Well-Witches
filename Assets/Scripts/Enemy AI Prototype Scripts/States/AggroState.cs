using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEditor;

public class AggroState : State
{
    private StateMachine stateMachine;
    private BaseEnemyClass enemy;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    public AggroState(GameObject owner) : base(owner) { }

    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        enemy = owner.GetComponent<BaseEnemyClass>();

        if (enemy)
        {
            agent.stoppingDistance = enemy.range;
            agent.speed = enemy.moveSpeed;
        }
    }

    public override void OnEnter()
    {
        if (!agent.isOnNavMesh) { enemy.Die(); return; }
        agent.isStopped = false;
        agent.speed = enemy.moveSpeed;
    }

    public override void OnUpdate()
    {
        if (enemy && !enemy.isStunned)
        {
            enemy.TargetClosestPlayer();
        }
        agent.SetDestination(enemy.currentTarget.position);
    }

    public override void OnExit()
    {
        agent.speed = 0f;
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new StunnedTransition(stateMachine, owner),
            new InRangeTransition(stateMachine, owner),
        };
    }
}