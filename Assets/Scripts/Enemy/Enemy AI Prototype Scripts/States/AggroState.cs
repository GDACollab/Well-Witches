using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AggroState : State
{
    private Rigidbody2D rb;
    private StateMachine stateMachine;
    private float moveSpeed;
    private float damage;
    private MeleeEnemy meleeEnemy;
    private RangedEnemy rangedEnemy;
    private TankEnemy tankEnemy;

    public Transform target;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    public AggroState(GameObject owner) : base(owner) { }

    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        rb = owner.GetComponent<Rigidbody2D>();
        meleeEnemy = owner.GetComponent<MeleeEnemy>();
        rangedEnemy = owner.GetComponent<RangedEnemy>();
        tankEnemy = owner.GetComponent<TankEnemy>();
        if (meleeEnemy != null)
        {
            moveSpeed = meleeEnemy.moveSpeed;
            agent.speed = moveSpeed;
        }
        else if (rangedEnemy != null)
        {
            moveSpeed = rangedEnemy.moveSpeed;
            agent.speed = moveSpeed;
            agent.stoppingDistance = rangedEnemy.range;
        }
        else if (tankEnemy != null)
        {
            moveSpeed = tankEnemy.moveSpeed;
            damage = tankEnemy.damage;
            agent.speed = moveSpeed;
        }

    }

    public override void OnEnter()
    {
        Debug.Log("Entering Patrol State");

    }

    public override void OnUpdate()
    {
        if (meleeEnemy != null)
        {
            meleeEnemy.TargetClosestPlayer();
            agent.SetDestination(meleeEnemy.currentTarget.position);
            //if (target != null)
            //{
            //    Vector2 targetPosition = new Vector2(target.position.x, target.position.y);
            //    Vector2 direction = (targetPosition - rb.position).normalized;
            //    rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
            //}
        }
        else if (rangedEnemy != null)
        {
            rangedEnemy.TargetClosestPlayer();
            agent.SetDestination(rangedEnemy.currentTarget.position);

        }
        else if (tankEnemy != null)
        {
            tankEnemy.Pursue();
            tankEnemy.SpawnPool();
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Aggro State");
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new InRangeTransition(stateMachine, owner)
        };
    }
}
