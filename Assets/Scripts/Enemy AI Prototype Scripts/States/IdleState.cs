using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(GameObject owner) : base(owner) { }

    public override void OnEnter()
    {
        Debug.Log("Entering Idle State");
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
        return new List<Transition>();
    }
}