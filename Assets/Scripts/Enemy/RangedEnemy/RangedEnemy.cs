using UnityEngine;

// Jim Lee <-- who to ask and blame if something here doesn't work

public class RangedEnemy : BaseEnemyClass
{
    [HideInInspector]
    private float projectileSpread;
    private float projectileSpeed;
    private float projectileSize;
    private float projectileLifetime;

    private float projectileCount;
    private float projectileDamage;
    private float AOESize;
    private float AOELifetime;
    private float AOEDamage;

    public Animator animator;
    //public SpriteRenderer sprite;

    [Header("Initialize")]
    [SerializeField] private GameObject projectilePrefab;

    private void Start()
    {
        stats = EnemySpawner.Instance.difficultyStats[EnemySpawner.Instance.currentDifficulty];
        health = stats.rangedHealth;
        moveSpeed = stats.rangedSpeed;
        range = stats.rangedRange;
        stunDuration = stats.stunDuration;

        projectileCount = stats.projectileCount;
        projectileDamage = stats.projectileDamage;
        timeBetweenAttack = stats.rangedTimeBetweenAttacks;
        projectileSpread = stats.projectileSpread;
        projectileSize = stats.projectileSize;
        projectileSpeed = stats.projectileSpeed;
        projectileLifetime = stats.projectileLifetime;

        AOESize = stats.AOESize;
        AOELifetime = stats.AOELifetime;
        AOEDamage = stats.AOEDamage;
    }

    public void Fire()
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

    // fires projectiles in a cone shape depending on the spread and projectile count
    public override void Attack()
    {
        //Currently animation does not play out for the attack
        //The atk swap between animation states is weird.
        animator.SetTrigger("Attacking");
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}

