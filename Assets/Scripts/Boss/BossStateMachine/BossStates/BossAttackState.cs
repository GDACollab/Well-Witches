using UnityEngine;

public class BossAttackState : BossState
{
    private float timer;

    public BossAttackState(BossEnemy bossEnemy, BossStateMachine bossStateMachine) : base(bossEnemy, bossStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        bossEnemy.GetAgent().isStopped = true;
        ChooseAttack();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        timer -= Time.deltaTime;
        if (bossEnemy.isStunned)
        {
            bossEnemy.StateMachine.ChangeState(bossEnemy.BossStunState);
            return;
        }
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

    private void ChooseAttack()
    {
        int choice = Random.Range(1, 3);

        if (Vector2.Distance(bossEnemy.transform.position, bossEnemy.currentTarget.position) <= 5)
        {
            switch (choice)
            {
                case 1:
                    Debug.Log("ShieldBash");

                    bossEnemy.bossShieldBash.PerformShieldBash();
                    timer = bossEnemy.bossShieldBash.attackDuration + bossEnemy.bossShieldBash.warningDuration + 1f;
                    break;
                case 2:
                    Debug.Log("SwordAttack");
                    bossEnemy.bossSwordAttack.PerformSwordAttack();
                    timer = bossEnemy.bossSwordAttack.attackDuration + bossEnemy.bossSwordAttack.warningDuration + 1f;
                    break;
                default:
                    Debug.Log("SwordAttackDefault");
                    bossEnemy.bossSwordAttack.PerformSwordAttack();
                    timer = bossEnemy.bossSwordAttack.attackDuration + bossEnemy.bossSwordAttack.warningDuration + 1f;
                    break;
            }
        }
        else
        {
            bossEnemy.bossLunge.PerformLunge();
            timer = bossEnemy.bossLunge.attackDuration + bossEnemy.bossLunge.warningDuration + 1f;
        }
    }

}
