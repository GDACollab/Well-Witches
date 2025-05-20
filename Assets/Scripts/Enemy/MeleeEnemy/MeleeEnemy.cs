using UnityEngine;
public class MeleeEnemy : BaseEnemyClass
{
    private float damage;

    [Header("Attack")]
    [Tooltip("Amount of time in seconds between an instance of damage")]
    public float timeBetweenAttack;
    [Tooltip("The higher the value larger the AOE indicated by the red circle")]
    public float attackAOE;
    [Tooltip("How fast the melee enemy moves while spinning")]
    public float speedWhileAttacking;

    private void Start()
    {
        stats = EnemySpawner.Instance.difficultyStats[EnemySpawner.Instance.currentDifficulty];
        health = stats.meleeHealth;
        moveSpeed = stats.meleeSpeed;
        damage = stats.meleeDamage;
    }

    public void Attack()
    {
        rb.velocity = (currentTarget.position - transform.position).normalized * speedWhileAttacking;
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.bruiserAttackSwipe, this.transform.position);

        // not very performantive, better if collider check but should be good enough
        if (Vector2.Distance(transform.position, currentTarget.position) < attackAOE)
        {
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackAOE);
    }
#endif
}
