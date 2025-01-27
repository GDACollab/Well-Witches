using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PatrolState : State
{
    private Rigidbody2D rb;
    [SerializeField] private float patrolSpeed = 2f;
    private Vector2 direction;
    private StateMachine stateMachine;

    public PatrolState(GameObject owner) : base(owner) { }

    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        rb = owner.GetComponent<Rigidbody2D>();
    }

    public override void OnEnter()
    {
        direction = Vector2.right; // Move in a straight line to the right

    }

    public override void OnUpdate()
    {

        rb.MovePosition(rb.position + direction * patrolSpeed * Time.deltaTime);
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Patrol State");
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new PatrolToIdleMouseClickTransition(stateMachine, owner)
        };
    }
}
