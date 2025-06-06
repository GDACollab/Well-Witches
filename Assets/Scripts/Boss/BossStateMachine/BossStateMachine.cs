using UnityEngine;

public class BossStateMachine
{ 
    public BossState CurrentBossState { get; set; }

    public void Initialize(BossState startingState)
    {
        CurrentBossState = startingState;
        CurrentBossState.EnterState();
    }

    public void ChangeState(BossState newState)
    {
        CurrentBossState.ExitState();
        CurrentBossState = newState;
        CurrentBossState.EnterState();
    }
}