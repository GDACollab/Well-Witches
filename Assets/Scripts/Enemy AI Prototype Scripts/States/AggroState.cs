using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;
using UnityEditor.ShaderGraph;

public class AggroState : State
{
    private Rigidbody2D rb;
    private StateMachine stateMachine;
    private float moveSpeed;
    private float damage;
    private MeleeEnemy meleeEnemy;
    private RangedEnemy rangedEnemy;
    private TankEnemy tankEnemy;

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
        }
        else if (rangedEnemy != null)
        {
            moveSpeed = rangedEnemy.moveSpeed;
        }
        else if (tankEnemy != null)
        {
            moveSpeed = tankEnemy.moveSpeed;
            damage = tankEnemy.damage;
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
            Transform target = meleeEnemy.currentTarget;
            if (target != null)
            {
                Vector2 targetPosition = new Vector2(target.position.x, target.position.y);
                Vector2 direction = (targetPosition - rb.position).normalized;
                rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
            }
        }
        else if (rangedEnemy != null)
        {
            rangedEnemy.TargetClosestPlayer();
            rangedEnemy.MoveRanged();
        }
        else if (tankEnemy != null)
        {
            tankEnemy.pursue();
            tankEnemy.spawnPool();
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Patrol State");
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new InRangeTransition(stateMachine, owner)
        };
    }
}