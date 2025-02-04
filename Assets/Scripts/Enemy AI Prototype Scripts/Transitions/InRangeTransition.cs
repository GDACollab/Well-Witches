using System.Collections.Generic;
using UnityEngine;

public class InRangeTransition : Transition
{
    private StateMachine stateMachine;
    private MeleeEnemy meleeEnemy;
    private RangedEnemy rangedEnemy;
    private AttackState attackState;

    public InRangeTransition(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        meleeEnemy = owner.GetComponent<MeleeEnemy>();
        rangedEnemy = owner.GetComponent<RangedEnemy>();
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
        return attackState;
    }
}