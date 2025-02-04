using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class OutotRangeTransition : Transition
{
    private GameObject owner;
    private StateMachine stateMachine;
    private AggroState aggroState;
    private AttackState attackState;
    private MeleeEnemy meleeEnemy;
    private RangedEnemy rangedEnemy;

    public OutotRangeTransition(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        aggroState = owner.GetComponent<AggroState>();
        attackState = owner.GetComponent<AttackState>();
        meleeEnemy = owner.GetComponent<MeleeEnemy>();
        rangedEnemy = owner.GetComponent<RangedEnemy>();
    }

    public override bool ShouldTransition()
    {
        if (aggroState != null && !attackState.isAttacking)
        {
            float distance = 0f;

            if (meleeEnemy != null && meleeEnemy.currentTarget != null)
            {
                distance = Vector2.Distance(owner.transform.position, meleeEnemy.currentTarget.position);
                return distance >= meleeEnemy.range + 1;
            }
            else if (rangedEnemy != null && rangedEnemy.currentTarget != null)
            {
                distance = Vector2.Distance(owner.transform.position, rangedEnemy.currentTarget.position);
                return distance >= rangedEnemy.range + 1;
            }
        }
        return false;
    }

    public override State GetNextState()
    {
        // Transition to the AggroState and pass both owner and player
        Debug.Log("OutotRangeTransition");
        aggroState.Initialize(stateMachine, owner);
        return aggroState;
    }
}