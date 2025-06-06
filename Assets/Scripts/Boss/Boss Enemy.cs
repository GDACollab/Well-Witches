using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : BaseEnemyClass
{
    [Header("BOSS INFO")]
    public float attackCooldown;
    public bool DPS_phase = false;

    [Header("BOSS References")]
    public Animator animator;
    public GameObject bossShield;

    #region StateMachineVariables
    public BossStateMachine StateMachine { get; set; }
    public BossChaseState BossChaseState { get; set; }
    public BossAttackState BossAttackState { get; set; }
    public BossStunState BossStunState { get; set; }
    #endregion


    public BossShieldBash bossShieldBash;
    public SwordAttack bossSwordAttack;
    public BossLunge bossLunge;


    private void Awake()
    {
        StateMachine = new BossStateMachine();

        BossChaseState = new BossChaseState(this, StateMachine);
        BossAttackState = new BossAttackState(this, StateMachine);
        BossStunState = new BossStunState(this, StateMachine);

        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentTarget = GameObject.Find("Gatherer").transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        StateMachine.Initialize(BossChaseState);
    }

    private void Update()
    {
        StateMachine.CurrentBossState.OnUpdate();

        if (currentTarget && !isStunned)
        {
            sr.flipX = transform.position.x > currentTarget.position.x ? false : true;
        }
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentBossState.OnPhysicsUpdate();
    }



    public NavMeshAgent GetAgent()
    {
        return agent;
    }


    public override void Attack()
    {
        return;
    }

    public float shield_damage_scalar = 0.05f;

    public override void TakeDamage(float amount, bool fromWardenProjectile = false)
    {

        //Reduces health by the amount entered in Unity, or by 5% of that health outside of DPS phase

        if (DPS_phase)
        {
            health -= amount;
        }
        else
        {
            health -= amount * shield_damage_scalar;
        }

        if (health <= 0)
        {
            Die();
        }
    }
    public override void Die(bool fromWardenProjectile = false)
    {
        Destroy(gameObject);
        Debug.Log("Boss dead yippee"); //Make boss drop quest item here.
        SceneHandler.Instance.ToEndingCutscene();
    }

    public override void ProjectileKnockback(Vector3 force)
    {
        return;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 5);
        Gizmos.DrawWireSphere(transform.position, 3);

    }
}