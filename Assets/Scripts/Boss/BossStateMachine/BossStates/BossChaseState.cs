using UnityEngine;

public class BossChaseState : BossState
{
    private Transform target;
    private float timer;


    public BossChaseState(BossEnemy bossEnemy, BossStateMachine bossStateMachine) : base(bossEnemy, bossStateMachine)
    {

    }

    public override void EnterState() 
    {
        base.EnterState();
        bossEnemy.GetAgent().isStopped = false;
        target = bossEnemy.currentTarget;
        timer = bossEnemy.attackCooldown;
        Debug.Log("Entering Chase State");
    }

    public override void OnUpdate() 
    {
        base.OnUpdate();
        bossEnemy.GetAgent().SetDestination(target.position);

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            bossEnemy.StateMachine.ChangeState(bossEnemy.BossAttackState);
        }
    }

    public override void OnPhysicsUpdate() { }

    public override void ExitState() 
    {
        base.ExitState();
    }
}
