using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class BossEnemy : BaseEnemyClass
{
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;
    [Tooltip("Time Between Attacks")]
    public float attackCooldown;
    [Tooltip("Phase 1 to 2 HP")]
    public float phaseHP;

    [Header("DEBUG")]
    public float distanceToPlayer1;
    public float distanceToPlayer2;
    public float distanceToTarget;
    public float timeToFire;
    [SerializeField] private GameObject[] players;
    public Transform currentTarget;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update()
    {

    }

    public void TargetClosestPlayer()
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

    public void Claw_attack()
    {
        Debug.Log("Claw Attack");
    }
    public void Cape_Swipe()
    {
        Debug.Log("Cape Swipe");
    }
    public void Spawn_Enemies()
    {
        Debug.Log("Spawn Enemies");
    }

    public override void TakeDamage(float amount)
    {   //Reduces health by the amount entered in Unity
        //Debug.Log("took damage");
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    public override void Die()
    {
        Destroy(gameObject);
        Debug.Log("Boss dead yippee"); //Make boss drop quest item here.
    }
}