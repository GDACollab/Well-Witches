using System.Collections;
using UnityEngine;

public class BossAttackState : BossState
{
    private float timer = 2f;
    
    public BossAttackState(BossEnemy bossEnemy, BossStateMachine bossStateMachine) : base(bossEnemy, bossStateMachine)
    {
    }

    public override void EnterState() 
    {
        base.EnterState();
        bossEnemy.GetAgent().isStopped = true;
        timer = 2f;
        bossEnemy.bossShieldBash.PerformShieldBash();
    }

    public override void OnUpdate() 
    {
        base.OnUpdate();
        timer -= Time.deltaTime;
        if (timer <= 0)
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
