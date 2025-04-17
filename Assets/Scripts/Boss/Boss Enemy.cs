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
    [Tooltip("The distance the Player has to be from the boss for the lunge attack to activiate")]
    public float LungeDistance;
    [Tooltip("How long the boss will shake to project the incoming lunge attack")]
    public float LungeBuildupTime;

    public float LungeSpeed;


    [Header("DEBUG")]
    public float distanceToPlayer1;
    public float distanceToPlayer2;
    public float distanceToTarget;
    public float timeToFire;
    [SerializeField] private GameObject[] players;
    public Transform currentTarget;

    private Rigidbody2D rb2d;
    private bool isLunging = false;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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

    public void LungeAttack()
    {
        if (!isLunging)
        {
            StartCoroutine(LungeCoroutine());
        }
    }

    private IEnumerator LungeCoroutine()
    {
        isLunging = true;

        // Shake effect during buildup time
        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;

        while (elapsedTime < LungeBuildupTime)
        {
            float shakeAmount = 0.1f; // Adjust the shake amount as needed
            transform.position = originalPosition + (Vector3)Random.insideUnitCircle * shakeAmount;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset position after shaking
        transform.position = originalPosition;

        // Move towards the current target at LungeAttackSpeed
        if (currentTarget != null)
        {
            Vector2 direction = (currentTarget.position - transform.position).normalized;
            float lungeDuration = 0.5f; // Adjust the lunge duration as needed
            elapsedTime = 0f;

            while (elapsedTime < lungeDuration)
            {
                rb2d.MovePosition(rb2d.position + direction * LungeSpeed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        isLunging = false;
    }

    public void Shield_Bash()
    {
        Debug.Log("Shield Bash");
    }
    public void Sword_Slash()
    {
        Debug.Log("Sword Slash");
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
}
