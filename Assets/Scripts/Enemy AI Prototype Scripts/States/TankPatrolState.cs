using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;

public class PatrolState : State
{
    private Rigidbody2D rb;
    // speed has been set to moveSpeed
    private float moveSpeed;
    private float damage;
    [Header("Targets")]
    [Tooltip("The game object 'gatherer'")]
    public GameObject gatherer;
    [Tooltip("The game object 'warden'")]
    public GameObject warden;
    private StateMachine stateMachine;
    [Header("Acid Pool")]
    [Range(0, 25)]
    [Tooltip("How many acid pools spawn per second. [0,25]")]
    public float spawnRate;
    [Range(0, 10)]
    [Tooltip("Size of the acid pool. [0,10]")]
    public float acidSize;
    [Range(0, 5)]
    [Tooltip("Time in seconds before the acid pool disapears. [0,5]")]
    public float acidLifetime;
    [Range(-5, 5)]
    [Tooltip("Move the spawn point of the acid pool left and right. [-5,5]")]
    public float acidOffsetX;
    [Range(-5, 5)]
    [Tooltip("Move the spawn point of the acid pool up and down. [-5,5]")]
    public float acidOffsetY;

    private GameObject target;
    private float timeTillPool;

    public PatrolState(GameObject owner) : base(owner) { }

    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        rb = owner.GetComponent<Rigidbody2D>();
        // get moveSpeed and Damage from BaseEnemyClass
        BaseEnemyClass baseEnemy = owner.GetComponent<BaseEnemyClass>();
        moveSpeed = baseEnemy.moveSpeed;
        damage = baseEnemy.damage;

    }

    public override void OnEnter()
    {
        Debug.Log("Entering Patrol State");

    }

    public override void OnUpdate()
    {
        // chase after the nearest player
        pursue();
        // spawn acid pools 
        spawnPool();
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Patrol State");
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition>
        {
            new PatrolToIdleMouseClickTransition(stateMachine, owner)
        };
    }

    // movement function
    private void pursue()
    {
        // set the target to the closest target
        findClosestTarget();
        // direction is the normalized vector between the enemy and target
        Vector2 direction = (new Vector2(target.transform.position.x, target.transform.position.y) - rb.position).normalized;
        // only move when far form the target
        if (Vector2.Distance(rb.position, target.transform.position) > 0.5f)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        }
        // For the code below, assume the enemy sprite is facing left, the code is dependent on the direction of the enemy its facing
        // If direction.x (as calculated above) is positive, that means the enemy is on the left side of the player
        if (direction.x > 0)
        {
            // We dont make changes to the sprite since its already facing the player
            transform.localScale = new Vector3(1, 1, 1);
            // If direction.x is negative (on the left of the player)
        }
        else if (direction.x < 0)
        {
            // We flip the enemy sprite so it faces the player
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // set the target to the closest target
    private void findClosestTarget()
    {
        // if both exist, target is the closer of the two.
        if (gatherer & warden)
        {
            if (Vector2.Distance(gatherer.transform.position, transform.position) < Vector2.Distance(warden.transform.position, transform.position))
            {
                target = gatherer;
            }
            else
            {
                target = warden;
            }
        } // otherwise if only one player exists set that one to the target
        else if (gatherer)
        {
            target = gatherer;
        } 
        else if (warden)
        {
            target = warden;
        }  
    }


    // spawns acid pool projectile 
    private void spawnPool()
    {
        if (timeTillPool <= 0)
        {
            
            // spawns acid pool
            GameObject acidPool = ProjectilePooling.SharedInstance.GetProjectileObject();
            if (acidPool)
            {
                acidPool.transform.position = new Vector2(acidOffsetX, acidOffsetY) + rb.position;
                acidPool.transform.localScale = Vector3.one * acidSize;
                acidPool.SetActive(true);
                acidPool.GetComponent<AcidPool>().
                    InitializeAcid(acidLifetime, damage);
            }
            timeTillPool = 1/spawnRate;
        }
        else
        {
            timeTillPool -= Time.deltaTime;
        }
    }
}
