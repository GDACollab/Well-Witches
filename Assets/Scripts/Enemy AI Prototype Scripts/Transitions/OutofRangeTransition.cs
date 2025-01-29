using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class OutotRangeTransition : Transition
{
    private GameObject owner;
    private StateMachine stateMachine;
    private AggroState aggroState;
    private AttackState attackState;



    public OutotRangeTransition(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        aggroState = owner.GetComponent<AggroState>();
        attackState = owner.GetComponent<AttackState>();
    }

    public override bool ShouldTransition()
    {
        if (aggroState != null && aggroState.target != null && !attackState.isAttacking)
        {
            float distance = Vector2.Distance(owner.transform.position, aggroState.target.transform.position);
            return distance >= attackState.attackRange + 1;
        }
        return false;
    }

    public override State GetNextState()
    {
        // Transition to the AttackState and pass both owner and player
        Debug.Log("OutotRangeTransition");
        aggroState.Initialize(stateMachine, owner);
        return aggroState;
    }
}