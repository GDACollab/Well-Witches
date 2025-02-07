using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnState : State
{
    [SerializeField] public EnemySpawner enemySpawner;

    private StateMachine stateMachine;
    public BossSpawnState(GameObject owner) : base(owner) {}
    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
    }

    public override void OnEnter()
    {
        Debug.Log("Summoning enemies!");
    }

    public override void OnUpdate()
    {
        // No additional logic for idling
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Idle State");
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new IdleToPatrolMouseClickTransition(stateMachine, owner)
        };
    }
}
