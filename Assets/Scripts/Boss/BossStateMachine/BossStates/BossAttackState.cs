using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        Debug.Log(choice);

        if (Vector2.Distance(bossEnemy.transform.position, bossEnemy.currentTarget.position) <= 5)
        {
            switch (choice)
            {
                case 1:
                    bossEnemy.bossShieldBash.PerformShieldBash();
                    timer = bossEnemy.bossShieldBash.attackDuration + bossEnemy.bossShieldBash.warningDuration + 1f;
                    break;
                case 2:
                    bossEnemy.bossSwordAttack.PerformSwordAttack();
                    timer = bossEnemy.bossSwordAttack.attackDuration + bossEnemy.bossSwordAttack.warningDuration + 1f;
                    break;
                default:
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
