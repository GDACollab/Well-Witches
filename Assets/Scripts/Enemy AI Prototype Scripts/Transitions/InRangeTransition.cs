using System.Collections.Generic;
using UnityEngine;

public class InRangeTransition : Transition
{
    private StateMachine stateMachine;
    private MeleeEnemy meleeEnemy;
    private RangedEnemy rangedEnemy;
    private TankEnemy tankEnemy;
    private AttackState attackState;

    public InRangeTransition(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        meleeEnemy = owner.GetComponent<MeleeEnemy>();
        rangedEnemy = owner.GetComponent<RangedEnemy>();
        tankEnemy = owner.GetComponent<TankEnemy>();
        attackState = owner.GetComponent<AttackState>();
    }

    public override bool ShouldTransition()
    {
        if (meleeEnemy != null && meleeEnemy.currentTarget != null)
        {
            float distance = Vector2.Distance(owner.transform.position, meleeEnemy.currentTarget.transform.position);
            return distance <= meleeEnemy.range;
        }
        else if (rangedEnemy != null && rangedEnemy.currentTarget != null)
        {
            float distance = Vector2.Distance(owner.transform.position, rangedEnemy.currentTarget.transform.position);
            return distance <= rangedEnemy.range;
        }
        else if (tankEnemy != null && tankEnemy.currentTarget != null)
        {
            float distance = Vector2.Distance(owner.transform.position, tankEnemy.currentTarget.transform.position);
            return distance <= tankEnemy.range;
        }
        return false;
    }

    public override State GetNextState()
    {
        // Transition to the AttackState and pass both owner and player
        Debug.Log("InRangeTransition");
        if (meleeEnemy != null)
        {
            attackState.Initialize(stateMachine, owner, meleeEnemy.currentTarget);
        }
        else if (rangedEnemy != null)
        {
            attackState.Initialize(stateMachine, owner, rangedEnemy.currentTarget);
        }
        else if (tankEnemy != null)
        {
            attackState.Initialize(stateMachine, owner, tankEnemy.currentTarget);
        }
        return attackState;
    }
}