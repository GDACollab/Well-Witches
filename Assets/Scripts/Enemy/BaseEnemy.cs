using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemyClass : MonoBehaviour
{
    [Header("Enemy Stats")]
    [Tooltip("The max health of an enemy.")]
    public float health;
    [Tooltip("How fast an enemy moves.")]
    public float moveSpeed;
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;
    public NavMeshAgent agent;
    public Rigidbody2D rb;
    SiphonEnergy siphon;

    [Header("DEBUG")]
    public float distanceToPlayer1;
    public float distanceToPlayer2;
    public float distanceToTarget;
    public float timeToFire;
    [SerializeField] private GameObject[] players;
    public Transform currentTarget;
    public void Spawn(Vector3 position)
    {
        Instantiate(gameObject, position, Quaternion.identity);
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        EnemySpawner.currentEnemyCount--;
        //if siphon energy is equipped then add to siphone times
        if (WardenAbilityManager.Instance.passiveAbilityName == WardenAbilityManager.Passive.SoulSiphon)
        {
            WardenAbilityManager.Instance.siphonTimes++;
        }
        Destroy(gameObject);
    }

    // calculates and set target to the closest player to the enemy
    public virtual void TargetClosestPlayer()
    {
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


    public virtual void ProjectileKnockback(Vector3 force)
    {
        agent.enabled = false;
        rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(EnableAgent());
    }
    
    IEnumerator EnableAgent()
    {
        yield return new WaitForSeconds(0.2f);
        agent.enabled = true;
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector3.zero;
    }

}
