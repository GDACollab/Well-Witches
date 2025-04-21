using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AggroState : State
{
    private Rigidbody2D rb;
    private StateMachine stateMachine;
    private float moveSpeed;
    private MeleeEnemy meleeEnemy;
    private RangedEnemy rangedEnemy;
    private TankEnemy tankEnemy;


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
            agent.stoppingDistance = meleeEnemy.range;
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
            agent.stoppingDistance = tankEnemy.range;
            agent.speed = moveSpeed;
        }

    }

    public override void OnEnter()
    {
        agent.enabled = true;
    }

    public override void OnUpdate()
    {
        if (meleeEnemy != null && meleeEnemy.GetComponent<NavMeshAgent>().enabled == true)
        {
            meleeEnemy.TargetClosestPlayer();
            agent.SetDestination(meleeEnemy.currentTarget.position);
        }
        else if (rangedEnemy != null)
        {
            rangedEnemy.TargetClosestPlayer();
            agent.SetDestination(rangedEnemy.currentTarget.position);
        }
        else if (tankEnemy != null)
        {
            tankEnemy.TargetClosestPlayer();
            agent.SetDestination(tankEnemy.currentTarget.position);
            tankEnemy.SpawnPool();
        }
    }

    public override void OnExit()
    {
        agent.enabled = false;
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new InRangeTransition(stateMachine, owner)
        };
    }
}