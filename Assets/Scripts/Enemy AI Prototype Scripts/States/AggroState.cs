using System.Collections.Generic;
using UnityEngine;

public class AggroState : State
{
    private Rigidbody2D rb;
    private float moveSpeed;
    private StateMachine stateMachine;
    private MeleeEnemy meleeEnemy;
    private RangedEnemy rangedEnemy;

    public AggroState(GameObject owner) : base(owner) { }

    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;

        rb = owner.GetComponent<Rigidbody2D>();
        meleeEnemy = owner.GetComponent<MeleeEnemy>();
        rangedEnemy = owner.GetComponent<RangedEnemy>();

        if (meleeEnemy != null)
        {
            moveSpeed = meleeEnemy.moveSpeed;
        }
        else if (rangedEnemy != null)
        {
            moveSpeed = rangedEnemy.moveSpeed;
        }
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Aggro State");
    }

    public override void OnUpdate()
    {
        if (meleeEnemy != null)
        {
            meleeEnemy.TargetClosestPlayer();
            Transform target = meleeEnemy.currentTarget;
            if (target != null)
            {
                Vector2 targetPosition = new Vector2(target.position.x, target.position.y);
                Vector2 direction = (targetPosition - rb.position).normalized;
                rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
            }
        }
        else if (rangedEnemy != null)
        {
            rangedEnemy.TargetClosestPlayer();
            Transform target = rangedEnemy.currentTarget;
            if (target != null)
            {
                Vector2 targetPosition = new Vector2(target.position.x, target.position.y);
                Vector2 direction = (targetPosition - rb.position).normalized;
                rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
            }
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Aggro State");
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new InRangeTransition(stateMachine, owner),
        };
    }
}