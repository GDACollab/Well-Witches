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
        bossEnemy.attackCooldown = Random.Range(1, 3);
    }

    public override void OnUpdate() 
    {
        base.OnUpdate();
        bossEnemy.GetAgent().SetDestination(target.position);

        timer -= Time.deltaTime;

        if (Vector2.Distance(bossEnemy.transform.position, bossEnemy.currentTarget.position) <= 2)
        {
            bossEnemy.StateMachine.ChangeState(bossEnemy.BossAttackState);
            return;
        }

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
