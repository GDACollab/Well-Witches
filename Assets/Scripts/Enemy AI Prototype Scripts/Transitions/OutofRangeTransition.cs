using UnityEngine;

public class OutotRangeTransition : Transition
{
    private GameObject owner;
    private StateMachine stateMachine;
    private AggroState aggroState;
    private AttackState attackState;
    private BaseEnemyClass enemy;

    public OutotRangeTransition(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        aggroState = owner.GetComponent<AggroState>();
        attackState = owner.GetComponent<AttackState>();

        enemy = owner.GetComponent<BaseEnemyClass>();
    }

    public override bool ShouldTransition()
    {
        if (aggroState != null)
        {
            float distance = 0f;
            if (enemy && enemy.currentTarget != null)
            {
                distance = Vector2.Distance(owner.transform.position, enemy.currentTarget.position);
                return distance >= enemy.range + 1;
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