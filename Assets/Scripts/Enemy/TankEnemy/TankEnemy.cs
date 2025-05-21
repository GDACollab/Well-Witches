using System.Collections;
using UnityEngine;


public class TankEnemy : BaseEnemyClass
{
    [HideInInspector]
    public float timeBetweenAttack;
    private float bashStrength;
    private float bashTime;
    private float bashDamage;

    public float spawnRate;
    public float acidOffsetX;
    public float acidOffsetY;

    private float acidLifetime;
    private float acidSize;
    private float acidDamage;

    private float timeTillPool;

    private void Start()
    {
        stats = EnemySpawner.Instance.difficultyStats[EnemySpawner.Instance.currentDifficulty];
        health = stats.tankHealth;
        moveSpeed = stats.tankSpeed;
        range = stats.tankRange;
        stunDuration = stats.stunDuration;

        timeBetweenAttack = stats.tankTimeBetweenBash;
        bashStrength = stats.tankBashStrength;
        bashTime = stats.tankBashTime;
        bashDamage = stats.tankBashDamage;
        
        acidSize = stats.tankAcidSize;
        acidLifetime = stats.tankAcidLifetime;
        acidDamage = stats.tankAcidDamage;
        timeTillPool = 0f;
    }

    public void Attack()
    {
        rb.velocity = (currentTarget.position - transform.position).normalized * bashStrength;
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.tankAttackBash, this.transform.position);
        StartCoroutine(EndBash());
    }

    IEnumerator EndBash()
    {
        yield return new WaitForSeconds(bashTime);
        rb.velocity = Vector2.zero;
    }

    public void SpawnPool()
    {
        if (timeTillPool <= 0)
        {
            // spawns acid pool
            GameObject acidPool = AcidTrailPooling.SharedInstance.GetAcidPoolObject();
            if (acidPool)
            {
                acidPool.transform.position = new Vector2(acidOffsetX, acidOffsetY) + rb.position;
                acidPool.transform.localScale = Vector3.one * acidSize;
                acidPool.SetActive(true);
                acidPool.GetComponent<AcidPool>().
                    InitializeAcid(acidLifetime, acidDamage);
            }
            timeTillPool = 1 / spawnRate;
        }
        else
        {
            timeTillPool -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.name == "Warden")
            {
                EventManager.instance.playerEvents.PlayerDamage(bashDamage, "Warden");
            }
            else if (collision.gameObject.name == "Gatherer")
            {
                EventManager.instance.playerEvents.PlayerDamage(bashDamage, "Gatherer");
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
