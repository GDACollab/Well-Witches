using UnityEngine;
public class MeleeEnemy : BaseEnemyClass
{
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;


    [Header("Attack")]
    public float damage;
    [Tooltip("Amount of time in seconds between an instance of damage")]
    public float timeBetweenAttack;
    [Tooltip("The higher the value larger the AOE indicated by the red circle")]
    public float attackAOE;
    [Tooltip("How fast the melee enemy moves while spinning")]
    public float speedWhileAttacking;

    [Header("DEBUG")]
    public float distanceToPlayer1;
    public float distanceToPlayer2;
    public float distanceToTarget;
    public float timeToFire;
    [SerializeField] private GameObject[] players;
    public Transform currentTarget;

    private Rigidbody2D rb2d;
    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {

    }

    // calculates and set target to the closest player to the enemy
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

    public void Attack()
    {
        if (Vector2.Distance(currentTarget.transform.position, transform.position) < attackAOE)
        {
            rb2d.velocity = (currentTarget.position - transform.position).normalized * speedWhileAttacking;
            if (currentTarget.gameObject.name == "Warden")
            {
                EventManager.instance.playerEvents.PlayerDamage(damage, "Warden");
            }
            else if (currentTarget.gameObject.name == "Gatherer")
            {
                EventManager.instance.playerEvents.PlayerDamage(damage, "Gatherer");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackAOE);
    }
}