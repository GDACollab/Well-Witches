using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AttackState : State
{
    private GameObject target;

    [Header("Attack Settings")]
    public float attackRange = 5f;
    public float attackRate = 2f;
    private int attackCount = 0;
    private float lastAttackTime;
    private StateMachine stateMachine;
    public bool isAttacking;




    public AttackState(GameObject owner, GameObject player) : base(owner) { }
    public void Initialize(StateMachine stateMachine, GameObject owner, GameObject target)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        this.target = target;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Attack State");
        lastAttackTime = Time.time - attackRate;
        isAttacking = false;

    }

    public override void OnUpdate()
    {
        if (target == null) return;

        if (Time.time >= lastAttackTime + attackRate && !isAttacking)
        {
            PerformAttack();
            lastAttackTime = Time.time;
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Attack State");
        attackCount = 0;
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new OutotRangeTransition(stateMachine, owner),
        };
    }

    private void PerformAttack()
    {
        isAttacking = true;

        Debug.Log("Performing regular attack");

        isAttacking = false;
    }
}