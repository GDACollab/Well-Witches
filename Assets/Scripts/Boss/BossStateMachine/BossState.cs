using UnityEngine;

public class BossState
{
    protected BossEnemy bossEnemy;
    protected BossStateMachine bossStateMachine;

    public BossState( BossEnemy bossEnemy,  BossStateMachine bossStateMachine)
    {
        this.bossEnemy = bossEnemy;
        this.bossStateMachine = bossStateMachine;
    }

    public virtual void EnterState() { }

    public virtual void OnUpdate() { }

    public virtual void OnPhysicsUpdate() { }

    public virtual void ExitState() { }



}
