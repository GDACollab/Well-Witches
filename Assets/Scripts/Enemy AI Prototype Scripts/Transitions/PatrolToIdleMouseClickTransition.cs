using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PatrolToIdleMouseClickTransition : Transition
{
    private GameObject owner;
    private StateMachine stateMachine;
    private IdleState idleState;



    public PatrolToIdleMouseClickTransition(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        idleState = owner.GetComponent<IdleState>();

    }

    public override bool ShouldTransition()
    {
        return Input.GetMouseButtonDown(0); // Detect mouse click
    }

    public override State GetNextState()
    {
        // Transition to the AttackState and pass both owner and player
        Debug.Log("PatrolToIdleMouseClickTransition");
        idleState.Initialize(stateMachine, owner);
        return idleState;
    }
}