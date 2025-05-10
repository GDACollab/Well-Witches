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
    private TankEnemy tankEnemy;

    public OutotRangeTransition(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        aggroState = owner.GetComponent<AggroState>();
        attackState = owner.GetComponent<AttackState>();
        meleeEnemy = owner.GetComponent<MeleeEnemy>();
        rangedEnemy = owner.GetComponent<RangedEnemy>();
        tankEnemy = owner.GetComponent<TankEnemy>();
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
            else if (tankEnemy != null && tankEnemy.currentTarget != null)
            {
                distance = Vector2.Distance(owner.transform.position, tankEnemy.currentTarget.position);
                return distance >= tankEnemy.range + 1;
            }
        }
        return false;
    }

    public override State GetNextState()
    {
        // Transition to the AggroState and pass both owner and player
        aggroState.Initialize(stateMachine, owner);
        return aggroState;
    }
}