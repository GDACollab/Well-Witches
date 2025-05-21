using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemyClass : MonoBehaviour
{
    [HideInInspector]
    public EnemyStatsSO stats;
        
    [Header("DEBUG")]
    public float health;
    public float moveSpeed;
    public float range;
    public bool isStunned;
    public float stunDuration;

    public float distanceToPlayer1;
    public float distanceToPlayer2;
    public float distanceToTarget;
    public float timeToFire;
    public GameObject[] players;
    public Transform currentTarget;
    public NavMeshAgent agent;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
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
        agent.enabled = false;
        rb.AddForce(force, ForceMode2D.Impulse);
        if (!isStunned) { StartCoroutine(EnableAgent()); }
    }
    
    IEnumerator EnableAgent()
    {
        yield return new WaitForSeconds(0.2f);
        agent.enabled = true;
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector3.zero;
    }

    public virtual void getStunned()
    {
        isStunned = true;
        agent.enabled = false;
        stunDuration = 5.0f;
    }

    void Update()
    {
        if (isStunned)
        {
            stunDuration -= Time.deltaTime;

            if (stunDuration <= 0.0f)
            {
                Debug.Log("timing out of stun");
                isStunned = false;
                StartCoroutine(EnableAgent());
            }
        }
    }
}
