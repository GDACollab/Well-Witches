using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AttackState : State
{

    [Header("Attack Settings")]
    private float lastAttackTime;
    private StateMachine stateMachine;
    public bool isAttacking;
    private MeleeEnemy meleeEnemy;
    private RangedEnemy rangedEnemy;
    private TankEnemy tankEnemy;
    private Rigidbody2D rb2d;


    public AttackState(GameObject owner, GameObject player) : base(owner) { }
    public void Initialize(StateMachine stateMachine, GameObject owner, Transform target)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        meleeEnemy = owner.GetComponent<MeleeEnemy>();
        rangedEnemy = owner.GetComponent<RangedEnemy>();
        tankEnemy = owner.GetComponent<TankEnemy>();
        rb2d = owner.GetComponent<Rigidbody2D>();

    }

    public override void OnEnter()
    {
        Debug.Log("Entering Attack State");
        lastAttackTime = Time.time - (meleeEnemy != null ? meleeEnemy.AttackRate : (rangedEnemy != null ? rangedEnemy.fireRate : tankEnemy.AttackRate));
        isAttacking = false;
        // Disable gravity to keep the enemy still

        if (rb2d != null)
        {
            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero; // Stop any existing movement
        }
    }

    public override void OnUpdate()
    {
        if (meleeEnemy != null)
        {
            if (Time.time >= lastAttackTime + meleeEnemy.AttackRate && !isAttacking)
            {
                meleeEnemy.TargetClosestPlayer();
                meleeEnemy.Attack();
                lastAttackTime = Time.time;
            }
        }
        else if (rangedEnemy != null)
        {
            if (Time.time >= lastAttackTime + rangedEnemy.fireRate && !isAttacking)
            {
                rangedEnemy.Attack();
                lastAttackTime = Time.time;
            }
        }
        else if (tankEnemy != null)
        {
            if (Time.time >= lastAttackTime + tankEnemy.AttackRate && !isAttacking)
            {
                tankEnemy.SpawnPool();
                tankEnemy.Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Attack State");
        // Re-enable gravity when exiting the attack state
        if (rb2d != null)
        {
            rb2d.gravityScale = 1;
        }
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new OutotRangeTransition(stateMachine, owner),
        };
    }
}