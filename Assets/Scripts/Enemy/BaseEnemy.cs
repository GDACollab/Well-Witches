using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseEnemyClass : MonoBehaviour
{
    [HideInInspector]
    public EnemyStatsSO stats;
    [HideInInspector]
    public float timeBetweenAttack;
    [HideInInspector]
    public float range;
    [HideInInspector]
    public bool isStunned;
    [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public float stunDuration;


    [Header("DEBUG")]
    public float health;

    protected float distanceToPlayer1;
    protected float distanceToPlayer2;
    protected float distanceToTarget;
    protected float timeToFire;
    protected GameObject[] players;
    public Transform currentTarget;
    protected NavMeshAgent agent;
    protected Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer sr;

    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        isStunned = false;
    }

    public void Spawn(Vector3 position)
    {
        Instantiate(gameObject, position, Quaternion.identity);
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
        if (WardenAbilityManager.Instance.GetEquippedPassiveName() == "WaterLogging")
        {
            StartCoroutine(slow());
        }
    }

    public virtual void Die()
    {
        EnemySpawner.currentEnemyCount--;
        //if siphon energy is equipped then add to siphone times
        if (WardenAbilityManager.Instance.passiveAbilityName == WardenAbilityManager.Passive.SoulSiphon)
        {
            SiphonEnergy.Instance.AddEnergy();
        }
        Destroy(gameObject);
    }

    public abstract void Attack();

    IEnumerator slow()
    {
        Debug.Log("SLOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOW");
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.blue;
        moveSpeed = moveSpeed * WardenAbilityManager.Instance.waterSpeed;
        yield return new WaitForSeconds(WardenAbilityManager.Instance.waterDuration);
        moveSpeed = moveSpeed / WardenAbilityManager.Instance.waterSpeed;
        sr.color = Color.white;
    }

    // calculates and set target to the closest player to the enemy
    // unless warden is dead then targets just gatherer
    public virtual void TargetClosestPlayer()
    {
        if (!isStunned)
        {
            if (StatsManager.Instance.WardenCurrentHealth <= 0f)
            {
                currentTarget = (players[0].gameObject.name == "Gatherer") ? players[0].transform : players[1].transform;
                return;
            }
            distanceToPlayer1 = Vector2.Distance(players[0].transform.position, transform.position);
            distanceToPlayer2 = Vector2.Distance(players[1].transform.position, transform.position);
            if (distanceToPlayer1 < distanceToPlayer2)
            {
                currentTarget = players[0].transform;
                distanceToTarget = distanceToPlayer1;
            }
            else
            {
                currentTarget = players[1].transform;
                distanceToTarget = distanceToPlayer2;
            }
        }
    }


    public virtual void ProjectileKnockback(Vector3 force)
    {
        agent.speed = 0;
        rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(ExitKnockback());
    }
    IEnumerator ExitKnockback()
    {
        yield return new WaitForSeconds(0.2f);

        if (isStunned)
        {
            rb.velocity = Vector3.zero;
            yield break;
        }

        rb.velocity = Vector3.zero;
        agent.speed = moveSpeed;
    }


    public virtual void getStunned()
    {
        isStunned = true;
    }

    void Update()
    {
        if (currentTarget && !isStunned)
        {
            sr.flipX = transform.position.x > currentTarget.position.x ? false : true;
        }
    }
}
