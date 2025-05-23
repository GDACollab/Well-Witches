using UnityEngine;

public class StunnedTransition : Transition
{
    private StateMachine stateMachine;
    private StunnedState stunnedState;
    private BaseEnemyClass enemy;

    public StunnedTransition(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.stateMachine = stateMachine;
        stunnedState = owner.GetComponent<StunnedState>();
        enemy = owner.GetComponent<BaseEnemyClass>();
    }

    public override bool ShouldTransition()
    {
        return enemy.isStunned;
    }
    public override State GetNextState()
    {
        if (enemy) { stunnedState.Initialize(stateMachine, owner); }
        return stunnedState;
    }

   
}
