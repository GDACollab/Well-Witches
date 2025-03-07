using System.Collections.Generic;
using UnityEngine;

public class PhaseOnetoTwo : Transition
{
    private StateMachine stateMachine;
    private BossEnemy bossEnemy;
    private PhaseTwo phaseTwo;
    public PhaseOnetoTwo(StateMachine stateMachine, GameObject owner) : base(owner)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        bossEnemy = owner.GetComponent<BossEnemy>();
        phaseTwo = owner.GetComponent<PhaseTwo>();
    }

    public override bool ShouldTransition()
    {
        return bossEnemy.health <= bossEnemy.phaseHP;
    }

    public override State GetNextState()
    {
        phaseTwo.Initialize(stateMachine, owner);
        return phaseTwo;
    }
}