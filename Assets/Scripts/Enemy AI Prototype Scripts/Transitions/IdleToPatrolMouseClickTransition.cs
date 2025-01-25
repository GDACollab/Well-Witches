using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class IdleToPatrolMouseClickTransition : Transition
{
    private GameObject owner;
    private StateMachine stateMachine;
    private PatrolState patrolState;

    public IdleToPatrolMouseClickTransition(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.stateMachine = stateMachine;
        this.patrolState = owner.GetComponent<PatrolState>();
        this.owner = owner;
    }

    public override bool ShouldTransition()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public override State GetNextState()
    {
        // Transition to the PatrolState and pass both owner and player
        Debug.Log("IdleToPatrolMouseClickTransition");
        patrolState.Initialize(stateMachine, owner);
        return patrolState;
    }
}
