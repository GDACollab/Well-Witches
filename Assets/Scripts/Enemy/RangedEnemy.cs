using UnityEngine;


public class RangedEnemy : BaseEnemyClass
{
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;
    [Tooltip("If enabled, enemy's speed will scale with distance from player with Move Speed being considered max speed.")]
    public bool PrototypeAdvancedMovement;

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
    public float projectileDamage;

    [Header("AOE")]
    [Tooltip("Size of the AOE when projectile lands")]
    public float AOESize;
    [Tooltip("How long the AOE lasts in seconds")]
    public float AOELifetime;
    public float AOEDamage;

    [Header("Initialize")]
    [SerializeField] private GameObject projectilePrefab;


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
        rb2d = GetComponent<Rigidbody2D>();
        players = GameObject.FindGameObjectsWithTag("Player");
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

    // fires projectiles in a cone shape depending on the spread and projectile count
    public void Attack()
    {
    }
}