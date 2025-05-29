using System.Collections;
using UnityEngine;


public class TankEnemy : BaseEnemyClass
{

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
    public Animator animator; 

    private void Start()
    {
        //animator = GetComponent<Animator>();
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

    public override void Attack()
    {
        //Animation to attack
        //print("Start animation");
        animator.SetTrigger("Attack");
        rb.AddForce((currentTarget.position - transform.position).normalized * bashStrength);
        agent.isStopped = true;
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.tankAttackBash, this.transform.position);
        StartCoroutine(EndBash());
    }

    IEnumerator EndBash()
    {
        //Animation to attack
        //print("End animation");
        //print(bashTime);
        //animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(bashTime);
        rb.velocity = Vector2.zero;
        agent.isStopped = false;
        agent.ResetPath();
    }

    public override void OnUpdate()
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
