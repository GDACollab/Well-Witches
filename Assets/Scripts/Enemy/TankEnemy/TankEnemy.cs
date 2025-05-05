using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class TankEnemy : BaseEnemyClass
{
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;


    [Header("Attack")]
    [Tooltip("The lower the value the faster the enemy fires projectiles")]
    public float AttackRate;

    [Header("DamageOfAcid")]
    [Tooltip("Acid Damage")]
    public float damage;

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

    [Header("DEBUG")]
    public float distanceToPlayer1;
    public float distanceToPlayer2;
    public float distanceToTarget;
    public float timeToFire;
    [SerializeField] private GameObject[] players;
    public Transform currentTarget;

    private float timeTillPool;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        players = GameObject.FindGameObjectsWithTag("Player");
    }


    private void Update()
    {
        
    }

    // calculates and set target to the closest player to the enemy
    private void TargetClosestPlayer()
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
        Debug.Log("Shield Bash");
    }

    public void SpawnPool()
    {
        if (timeTillPool <= 0)
        {

            // spawns acid pool
            GameObject acidPool = AcidTrailPooling.SharedInstance.GetAcidPoolObject();
            if (acidPool)
            {
                acidPool.transform.position = new Vector2(acidOffsetX, acidOffsetY) + rb2d.position;
                acidPool.transform.localScale = Vector3.one * acidSize;
                acidPool.SetActive(true);
                acidPool.GetComponent<AcidPool>().
                    InitializeAcid(acidLifetime, damage);
            }
            timeTillPool = 1 / spawnRate;
        }
        else
        {
            timeTillPool -= Time.deltaTime;
        }
    }

    public void Pursue()
    {
        // set the target to the closest target
        TargetClosestPlayer();
        Vector3 direction = currentTarget.position - transform.position;
        rb2d.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;
        //// direction is the normalized vector between the enemy and target
        //Vector2 direction = (new Vector2(currentTarget.transform.position.x, currentTarget.transform.position.y) - rb2d.position).normalized;
        //// only move when far form the target
        //if (Vector2.Distance(rb2d.position, currentTarget.transform.position) > 0.5f)
        //{
        //    rb2d.MovePosition(rb2d.position + direction * moveSpeed * Time.deltaTime);
        //}
        //// For the code below, assume the enemy sprite is facing left, the code is dependent on the direction of the enemy its facing
        //// If direction.x (as calculated above) is positive, that means the enemy is on the left side of the player
        //if (direction.x > 0)
        //{
        //    // We dont make changes to the sprite since its already facing the player
        //    transform.localScale = new Vector3(1, 1, 1);
        //    // If direction.x is negative (on the left of the player)
        //}
        //else if (direction.x < 0)
        //{
        //    // We flip the enemy sprite so it faces the player
        //    transform.localScale = new Vector3(-1, 1, 1);
        //}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.name == "Gatherer")
            {
                EventManager.instance.playerEvents.PlayerDamage(damage, "Gatherer");
            }
            else if (collision.gameObject.name == "Warden")
            {
                EventManager.instance.playerEvents.PlayerDamage(damage, "Warden");
            }
        }
    }
}