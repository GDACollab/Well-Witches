using UnityEngine;

// Jim Lee <-- who to ask and blame if something here doesn't work

public class RangedEnemy : BaseEnemyClass
{

    [Header("Projectile")]
    [Tooltip("Time between projectile firing in seconds")]
    public float timeBetweenAttack;
    [Tooltip("Angle of the projectile's spread, the larger the wider the spread")]
    [Range(0, 90)]
    public float projectileSpread;
    [Tooltip("Speed of the projectile")]
    public float projectileSpeed;
    [Tooltip("Size of the projectile")]
    public float projectileSize;
    [Tooltip("Time in seconds before the projectile explodes")]
    public float projectileLifetime;

    private float projectileCount;
    private float projectileDamage;
    private float AOESize;
    private float AOELifetime;
    private float AOEDamage;


    [Header("Initialize")]
    [SerializeField] private GameObject projectilePrefab;

    private void Start()
    {
        stats = EnemySpawner.Instance.difficultyStats[EnemySpawner.Instance.currentDifficulty];
        health = stats.rangedHealth;
        projectileCount = stats.projectileCount;
        projectileDamage = stats.projectileDamage;
        AOESize = stats.AOESize;
        AOELifetime= stats.AOELifetime;
        AOEDamage = stats.AOEDamage;
    }

    // fires projectiles in a cone shape depending on the spread and projectile count
    public void Attack()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            // spawns the projectile
            GameObject projectile = ProjectilePooling.SharedInstance.GetProjectileObject();
            if (projectile != null)
            {
                // offset of the projectile based on count and spread
                // used in InitializeProjectile() to calculate proper direction and projectile rotation
                float offset = (i - (projectileCount / 2)) * projectileSpread;
                projectile.transform.position = transform.position;
                projectile.transform.localScale = Vector3.one * projectileSize;
                projectile.SetActive(true);
                projectile.GetComponent<EnemyProjectile>().
                    InitializeProjectile(currentTarget.transform.position, offset, projectileSpeed, projectileLifetime, projectileDamage, AOESize, AOELifetime, AOEDamage);
            }
        }

    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}

