using System.Collections.Generic;
using UnityEngine;

public class PhaseOne : State
{
    private Rigidbody2D rb;
    private StateMachine stateMachine;
    private BossEnemy bossEnemy;
    private float P1attackCooldown = 0;

    private bool useShieldBash = true; // Flag to alternate between attacks


    private BossShieldBash shieldBash;
    private SwordAttack swordAttack;

    private bool isAnyAbilityCasting = false; 
    public PhaseOne(GameObject owner) : base(owner) { }

    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        rb = owner.GetComponent<Rigidbody2D>();
        bossEnemy = owner.GetComponent<BossEnemy>();
        swordAttack = owner.GetComponent<SwordAttack>();
        shieldBash = owner.GetComponent<BossShieldBash>();
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
            float distanceToTarget = Vector2.Distance(bossEnemy.transform.position, bossEnemy.currentTarget.position);

            if (distanceToTarget >= bossEnemy.LungeDistance)
            {
                bossEnemy.LungeAttack();
                Debug.Log("Lunge Attack");
            }
            else if (distanceToTarget > bossEnemy.range)
            {
                // Only move if no ability is being cast
                if (!isAnyAbilityCasting)
                {
                    Vector2 direction = (bossEnemy.currentTarget.position - bossEnemy.transform.position).normalized;
                    rb.MovePosition(rb.position + direction * bossEnemy.moveSpeed * Time.deltaTime);
                }
                else
                {
                    //Stop movement and rotation
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;

                }

            }
            else
            {
                if (P1attackCooldown <= 0)
                {
                    // Alternate between Shield_Bash and Sword_Slash when in range
                    if (useShieldBash)
                    {
                        //shieldBash.PerformShieldBash();
                        shieldBash.PerformShieldBash();
                    }
                    else
                    {
                        swordAttack.PerformSwordAttack();
                    }
                    // Reset the attack cooldown
                    P1attackCooldown = bossEnemy.attackCooldown;
                    // Toggle the flag for the next update
                    useShieldBash = !useShieldBash;
                }
            }
        }

        // Decrement the attack cooldown
        if (P1attackCooldown > 0)
        {
            P1attackCooldown -= Time.deltaTime;
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Phase One");
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new PhaseOnetoTwo(stateMachine, owner)
        };
    }

    public void SetAbilityCasting(bool isCasting)
    {
        isAnyAbilityCasting = isCasting;
    }
}