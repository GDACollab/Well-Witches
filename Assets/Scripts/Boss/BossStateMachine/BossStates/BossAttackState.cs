using UnityEngine;

public class BossAttackState : BossState
{
    public BossAttackState(BossEnemy bossEnemy, BossStateMachine bossStateMachine) : base(bossEnemy, bossStateMachine)
    {
    }

    public override void EnterState() 
    {
        base.EnterState();
        bossEnemy.GetAgent().isStopped = true;
        bossEnemy.bossShieldBash.PerformShieldBash();
    }

    public override void OnUpdate() 
    {
        base.OnUpdate();
        if (bossEnemy.bossShieldBash.isCasting == false)
        {
            bossEnemy.StateMachine.ChangeState(bossEnemy.BossChaseState);
        }
    }

    public override void OnPhysicsUpdate() 
    {
        base.OnPhysicsUpdate();
    }

    public override void ExitState() 
    {
        base.ExitState();

        bossEnemy.GetAgent().isStopped = false;
    }
}
