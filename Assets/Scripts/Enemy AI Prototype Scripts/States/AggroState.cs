using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AggroState : State
{
    private Rigidbody2D rb;
    private StateMachine stateMachine;
    private float moveSpeed;
    private float damage;
    private MeleeEnemy meleeEnemy;
    private RangedEnemy rangedEnemy;
    private TankEnemy tankEnemy;

    public AggroState(GameObject owner) : base(owner) { }

    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        rb = owner.GetComponent<Rigidbody2D>();
        meleeEnemy = owner.GetComponent<MeleeEnemy>();
        rangedEnemy = owner.GetComponent<RangedEnemy>();
        tankEnemy = owner.GetComponent<TankEnemy>();
        if (meleeEnemy != null)
        {
            moveSpeed = meleeEnemy.moveSpeed;
        }
        else if (rangedEnemy != null)
        {
            moveSpeed = rangedEnemy.moveSpeed;
        }
        else if (tankEnemy != null)
        {
            moveSpeed = tankEnemy.moveSpeed;
            damage = tankEnemy.damage;
        }

    }

    public override void OnEnter()
    {
        Debug.Log("Entering Patrol State");

    }

    public override void OnUpdate()
    {
        if (meleeEnemy != null)
        {
            meleeEnemy.TargetClosestPlayer();
            meleeEnemy.AggroMove();
        }
        else if (rangedEnemy != null)
        {
            rangedEnemy.TargetClosestPlayer();
            rangedEnemy.MoveRanged();
        }
        else if (tankEnemy != null)
        {
            tankEnemy.Pursue();
            tankEnemy.SpawnPool();
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
            new InRangeTransition(stateMachine, owner)
        };
    }
}
