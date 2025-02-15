using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PhaseOne : State
{
    private Rigidbody2D rb;
    private StateMachine stateMachine;



    public PhaseOne(GameObject owner) : base(owner) { }

    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        rb = owner.GetComponent<Rigidbody2D>();

    }

    public override void OnEnter()
    {
        Debug.Log("Entering Patrol State");

    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        Debug.Log("Exiting Patrol State");
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new PhaseOnetoTwo(stateMachine, owner)
        };
    }
}
