using UnityEngine;

public class BossStunState : BossState
{
    private float timer;

    public BossStunState(BossEnemy bossEnemy, BossStateMachine bossStateMachine) : base(bossEnemy, bossStateMachine)
    {

    }

    public override void EnterState() 
    {
        base.EnterState();
        bossEnemy.GetAgent().isStopped = true;
        timer = 2f;
    }

    public override void OnUpdate() 
    {
        base.OnUpdate();
       
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            bossEnemy.StateMachine.ChangeState(bossEnemy.BossChaseState);
        }
    }

    public override void OnPhysicsUpdate() { }

    public override void ExitState() 
    {
        base.ExitState();
        bossEnemy.isStunned = false;
    }
}
