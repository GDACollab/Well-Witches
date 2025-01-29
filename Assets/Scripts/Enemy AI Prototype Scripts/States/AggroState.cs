using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AggroState : State
{
    private Rigidbody2D rb;
    private float moveSpeed;
    public GameObject target;
    private StateMachine stateMachine;

    public AggroState(GameObject owner) : base(owner) { }

    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;

        rb = owner.GetComponent<Rigidbody2D>();
        BaseEnemyClass baseEnemy = owner.GetComponent<BaseEnemyClass>();
        moveSpeed = baseEnemy.moveSpeed;

    }

    public override void OnEnter()
    {
        Debug.Log("Entering Aggro State");

    }

    public override void OnUpdate()
    {
        if (target != null)
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.y);
            Vector2 direction = (targetPosition - rb.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Patrol State");
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            //new PatrolToIdleMouseClickTransition(stateMachine, owner),
            new InRangeTransition(stateMachine, owner),
        };
    }
}
