using UnityEngine;

public class BossChaseState : BossState
{
    private Transform target;



    public BossChaseState(BossEnemy bossEnemy, BossStateMachine bossStateMachine) : base(bossEnemy, bossStateMachine)
    {

    }

    public override void EnterState() 
    {
        base.EnterState();
        target = bossEnemy.currentTarget;
    }

    public override void OnUpdate() 
    {
        base.OnUpdate();
        Debug.Log(target.name);
        bossEnemy.agent.SetDestination(target.position);
    }

    public override void OnPhysicsUpdate() { }

    public override void ExitState() { }
}
