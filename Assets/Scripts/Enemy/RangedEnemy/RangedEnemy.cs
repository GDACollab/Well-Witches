using UnityEditor.SearchService;
using UnityEngine;


public class RangedEnemy : BaseEnemyClass
{
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;
    [Tooltip("How fast the enemy slows down/speeds up when moving towards player")]
    public float acceleration;

    [Header("Projectile")]
    [Tooltip("The lower the value the faster the enemy fires projectiles")]
    public float fireRate;
    [Tooltip("Amount of projectiles to fire out")]
    public float projectileCount;
    [Tooltip("Angle of the projectile's spread, the larger the wider the spread")]
    [Range(0, 90)]
    public float projectileSpread;
    [Tooltip("Speed of the projectile")]
    public float projectileSpeed;
    [Tooltip("Size of the projectile")]
    public float projectileSize;
    [Tooltip("Time in seconds before the projectile explodes")]
    public float projectileLifetime;
     
    [Header("AOE")]
    [Tooltip("Size of the AOE when projectile lands")]
    public float AOESize;
    [Tooltip("How long the AOE lasts in seconds")]
    public float AOELifetime;

    [Header("Initialize")]
    [SerializeField] private GameObject projectilePrefab;


    [Header("DEBUG")]
    public float distanceToPlayer1;
    public float distanceToPlayer2;
    public float timeToFire;
    [SerializeField] private GameObject[] players;
    public Transform currentTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        players = GameObject.FindGameObjectsWithTag("Player");
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
                GameObject projectile = ProjectilePooling.SharedInstance.GetProjectileObject();
                if (projectile != null)
                {
                    float offset = (i - (projectileCount / 2)) * projectileSpread;
                    projectile.transform.position = transform.position;
                    projectile.transform.localScale = Vector3.one * projectileSize;
                    projectile.SetActive(true);
                    projectile.GetComponent<Projectile>().
                        InitializeProjectile(currentTarget.transform.position, offset, projectileSpeed, projectileLifetime, AOESize, AOELifetime);
                }
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
        distanceToPlayer1 = Vector2.Distance(players[0].transform.position, transform.position);
        distanceToPlayer2 = Vector2.Distance(players[1].transform.position, transform.position);
        if (distanceToPlayer1 < distanceToPlayer2) 
        {
            currentTarget = players[0].transform;
        } else
        {
            currentTarget = players[1].transform;
        }
    }



}
