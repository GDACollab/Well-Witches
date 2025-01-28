using UnityEngine;


public class RangedEnemy : BaseEnemyClass
{
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;

    [Header("Projectile")]
    [Tooltip("The lower the value the faster the enemy fires projectiles")]
    public float fireRate;
    [Tooltip("Amount of projectiles to fire out")]
    public float projectileCount;
    [Tooltip("Angle of the projectile's spread")]
    [Range(0, 90)]
    public float projectileSpread;
    [Tooltip("Speed of the projectile")]
    public float projectileSpeed;

    [Header("Initialize")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;

    [Header("DEBUG")]
    public float distance;
    public float timeToFire;
    public Transform currentTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        TargetClosestPlayer();
        if (Vector2.Distance(transform.position, currentTarget.position) > range)
        {
            MoveRanged();
        } else
        {
            rb.velocity = Vector2.zero;
            Attack();
        }
    }

    // moves target towards player (pls let me override base class Move()
    // TODO: slow down when near player, maybe add variation too?
    public void MoveRanged()
    {
        Vector3 direction = currentTarget.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;
    }

    // fires projectiles in a cone shape depending on the spread and projectile count
    private void Attack()
    {
        if (timeToFire <= 0) 
        {
            for (int i = 0; i < projectileCount; i++)
            {
                // offset of the projectile based on count and spread
                // used in InitializeProjectile() to calculate proper direction and projectile rotation
                float offset = (i - (projectileCount / 2)) * projectileSpread;
                Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>().InitializeProjectile(currentTarget.transform.position, offset, projectileSpeed);
            }
            timeToFire = fireRate;
        } else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    // targets the closest player to the enemy
    private void TargetClosestPlayer()
    {
        float distanceToPlayer1 = Vector2.Distance(player1.transform.position, transform.position);
        float distanceToPlayer2 = Vector2.Distance(player2.transform.position, transform.position);
        if (distanceToPlayer1 < distanceToPlayer2) 
        {
            currentTarget = player1.transform;
        } else
        {
            currentTarget = player2.transform;
        }
    }



}
