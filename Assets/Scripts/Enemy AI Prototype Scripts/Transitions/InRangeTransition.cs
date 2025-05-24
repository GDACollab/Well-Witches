using UnityEngine;

public class InRangeTransition : Transition
{
    private StateMachine stateMachine;
    private BaseEnemyClass enemy;
    private AttackState attackState;

    public InRangeTransition(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        enemy = owner.GetComponent<BaseEnemyClass>();
        attackState = owner.GetComponent<AttackState>();
    }

    public override bool ShouldTransition()
    {
        if (enemy.currentTarget != null && !enemy.isStunned)
        {
            float distance = Vector2.Distance(owner.transform.position, enemy.currentTarget.transform.position);
            return distance <= enemy.range;
        }
        return false;
    }

    public override State GetNextState()
    {
        // Transition to the AttackState and pass both owner and player
        if (enemy != null) { attackState.Initialize(stateMachine, owner, enemy.currentTarget); }
        return attackState;
    }
}