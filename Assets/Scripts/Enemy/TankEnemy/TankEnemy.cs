using System.Collections;
using UnityEngine;


public class TankEnemy : BaseEnemyClass
{
    [Header("Bash Attack")]
    [Tooltip("Time between shield bash in seconds")]
    public float timeBetweenAttack;
    [Tooltip("How strongly the tank enemy launches during shield bash")]
    public float bashStrength;
    [Tooltip("How long the bash lasts")]
    public float bashTime;
    public float bashDamage;

    [Header("Acid Pool")]
    [Tooltip("How many acid pools spawn per second. [0,25]")]
    public float spawnRate;
    [Tooltip("Size of the acid pool. [0,10]")]
    public float acidSize;
    [Tooltip("Time in seconds before the acid pool disapears. [0,5]")]
    public float acidLifetime;
    [Range(-5, 5)]
    [Tooltip("Move the spawn point of the acid pool left and right. [-5,5]")]
    public float acidOffsetX;
    [Range(-5, 5)]
    [Tooltip("Move the spawn point of the acid pool up and down. [-5,5]")]
    public float acidOffsetY;

    private float timeTillPool;
    private float acidDamage;
    private void Start()
    {
        stats = EnemySpawner.Instance.difficultyStats[EnemySpawner.Instance.currentDifficulty];
        health = stats.tankHealth;
        acidDamage = stats.acidDamage;
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
