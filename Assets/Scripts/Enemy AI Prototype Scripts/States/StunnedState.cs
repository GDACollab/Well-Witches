using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class StunnedState : State
{
    private StateMachine stateMachine;
    private BaseEnemyClass enemy;
    private NavMeshAgent agent;
    private float timer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public StunnedState (GameObject owner) : base(owner) { }
    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        enemy = GetComponent<BaseEnemyClass>();
    }

    public override void OnEnter()
    {
        agent.speed = 0;
        timer = Time.time;
    }

    public override void OnUpdate()
    {
        if (Time.time - timer >= enemy.stunDuration)
        {
            enemy.isStunned = false;
        } 
    }

    public override void OnExit()
    {
        timer = 0f;
        agent.speed = enemy.moveSpeed;
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new OutotRangeTransition(stateMachine, owner)
        };
    }
}
