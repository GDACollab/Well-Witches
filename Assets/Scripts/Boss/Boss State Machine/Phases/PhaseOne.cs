using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PhaseOne : State
{
    private Rigidbody2D rb;
    private StateMachine stateMachine;
    private BossEnemy bossEnemy;

    private bool useShieldBash = true; // Flag to alternate between attacks


    public PhaseOne(GameObject owner) : base(owner) { }

    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        rb = owner.GetComponent<Rigidbody2D>();
        bossEnemy = owner.GetComponent<BossEnemy>();
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Phase One");

    }

    public override void OnUpdate()
    {
        bossEnemy.TargetClosestPlayer();
        if (bossEnemy.currentTarget != null)
        {
            if (bossEnemy.distanceToTarget >= bossEnemy.LungeDistance)
            {
                bossEnemy.LungeAttack();
                Debug.Log("Lunge Attack");
            }
            else
            {
                // Move towards the player
                Vector2 direction = (bossEnemy.currentTarget.position - bossEnemy.transform.position).normalized;
                rb.MovePosition(rb.position + direction * bossEnemy.moveSpeed * Time.deltaTime);
                Debug.Log("Moving towards player");
            }

            // Alternate between Shield_Bash and Sword_Slash when in range
            if (bossEnemy.distanceToTarget <= bossEnemy.range)
            {
                if (useShieldBash)
                {
                    bossEnemy.Shield_Bash();

                }
                else
                {
                    bossEnemy.Sword_Slash();
                }
                // Toggle the flag for the next update
                useShieldBash = !useShieldBash;
            }
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
            new PhaseOnetoTwo(stateMachine, owner)
        };
    }
}
