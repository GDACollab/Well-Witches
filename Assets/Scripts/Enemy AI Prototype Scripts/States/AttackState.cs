using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System.Transactions;

public class AttackState : State
{

    [Header("Attack Settings")]
    private StateMachine stateMachine;
    public bool isAttacking;
    private MeleeEnemy meleeEnemy;
    private RangedEnemy rangedEnemy;
    private TankEnemy tankEnemy;
    private Rigidbody2D rb2d;
    private NavMeshAgent agent;

    [Header("Debug")]
    [SerializeField] private float attackTime;


    public AttackState(GameObject owner, GameObject player) : base(owner) { }
    public void Initialize(StateMachine stateMachine, GameObject owner, Transform target)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        meleeEnemy = owner.GetComponent<MeleeEnemy>();
        rangedEnemy = owner.GetComponent<RangedEnemy>();
        tankEnemy = owner.GetComponent<TankEnemy>();
        rb2d = owner.GetComponent<Rigidbody2D>();
        agent = owner.GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Attack State");
        attackTime = Time.time - (meleeEnemy != null ? meleeEnemy.timeBetweenAttack : (rangedEnemy != null ? rangedEnemy.timeBetweenAttack : tankEnemy.timeBetweenAttack));
        attackTime = 0f;
        isAttacking = false;
        agent.enabled = false;

        if (rb2d != null)
        {
            {
                rb2d.gravityScale = 0;
                rb2d.velocity = Vector2.zero; // Stop any existing movement
            }
        }
    }

    public override void OnUpdate()
    {
        // apparently Time.deltaTime doesn't work in OnUpdate. this makes me extremely uncomfortable :(
        if (meleeEnemy != null)
        {
            if (Time.time >= attackTime + meleeEnemy.timeBetweenAttack && !isAttacking)
            {
                meleeEnemy.Attack();
                attackTime = Time.time;
            }
        }
        else if (rangedEnemy != null)
        {
            if (Time.time >= attackTime + rangedEnemy.timeBetweenAttack && !isAttacking)
            {
                rangedEnemy.Attack();
                attackTime = Time.time;
            }
        }
        else if (tankEnemy != null)
        {
            if (Time.time >= attackTime + tankEnemy.timeBetweenAttack && !isAttacking)
            {
                tankEnemy.Attack();
                attackTime = Time.time;
            }
        }
    }

    public override void OnExit()
    {
        agent.enabled = true;
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new OutotRangeTransition(stateMachine, owner),
        };
    }
}