using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class InRangeTransition : Transition
{
    private StateMachine stateMachine;
    private AggroState aggroState;
    private AttackState attackState;



    public InRangeTransition(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        aggroState = owner.GetComponent<AggroState>();
        attackState = owner.GetComponent<AttackState>();

    }

    public override bool ShouldTransition()
    {
        if (aggroState != null && aggroState.target != null)
        {
            float distance = Vector2.Distance(owner.transform.position, aggroState.target.transform.position);
            return distance <= attackState.attackRange;
        }
        return false;
    }

    public override State GetNextState()
    {
        // Transition to the AttackState and pass both owner and player
        Debug.Log("InRangeTransition");
        attackState.Initialize(stateMachine, owner, aggroState.target);
        return attackState;
    }
}