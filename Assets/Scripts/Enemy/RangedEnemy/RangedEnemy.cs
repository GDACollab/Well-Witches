using FMOD.Studio;
using UnityEngine;
using FMODUnity;

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
    private EventInstance rangedTraverseSFX;

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

        rangedTraverseSFX = AudioManager.Instance.CreateEventInstance(FMODEvents.Instance.rangedTraversal);
        rangedTraverseSFX.start();
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

                AudioManager.Instance.PlayOneShot(FMODEvents.Instance.rangedAttackFire, this.transform.position);
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
    override public void Die(bool fromWardenProjectile = false)
    {
        EnemySpawner.currentEnemyCount--;
        //if siphon energy is equipped and enemy is killed from warden's projectile, then siphon energy
        if (fromWardenProjectile && WardenAbilityManager.Instance.passiveAbilityName == WardenAbilityManager.Passive.SoulSiphon)
        {
            SiphonEnergy.Instance.AddEnergy();
        }
        rangedTraverseSFX.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        rangedTraverseSFX.release();

        Destroy(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}

