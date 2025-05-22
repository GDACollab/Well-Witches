using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AggroState : State
{
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
        if (meleeEnemy != null && agent.enabled == true && !meleeEnemy.isStunned)
        {
            meleeEnemy.TargetClosestPlayer();
            try { agent.SetDestination(meleeEnemy.currentTarget.position); } catch { meleeEnemy.Die(); }
        }
        else if (rangedEnemy != null && agent.enabled == true && !rangedEnemy.isStunned)
        {
            rangedEnemy.TargetClosestPlayer();
            try { agent.SetDestination(rangedEnemy.currentTarget.position); } catch { rangedEnemy.Die(); }
        }
        else if (tankEnemy != null && agent.enabled == true && !tankEnemy.isStunned)
        {
            tankEnemy.TargetClosestPlayer();
            tankEnemy.SpawnPool();
            try { agent.SetDestination(tankEnemy.currentTarget.position); } catch { tankEnemy.Die(); }
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