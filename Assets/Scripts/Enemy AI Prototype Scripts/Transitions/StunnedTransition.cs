using UnityEngine;

public class StunnedTransition : Transition
{
    private StateMachine stateMachine;
    private StunnedState stunnedState;
    private BaseEnemyClass enemy;

    public StunnedTransition(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        stunnedState = owner.GetComponent<StunnedState>();
        enemy = owner.GetComponent<BaseEnemyClass>();
    }

    public override bool ShouldTransition()
    {
        return enemy.isStunned;
    }
    public override State GetNextState()
    {
        stunnedState.Initialize(stateMachine, owner);
        return stunnedState;
    }

   
}
