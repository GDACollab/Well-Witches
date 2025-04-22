using FMODUnity;
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
    public float acidDamage;

    [Header("DEBUG")]
    public float distanceToPlayer1;
    public float distanceToPlayer2;
    public float distanceToTarget;
    public float timeToFire;
    [SerializeField] private GameObject[] players;
    public Transform currentTarget;

    private float timeTillPool;

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

    public void Attack()
    {
        rb2d.velocity = (currentTarget.position - transform.position).normalized * bashStrength;
        StartCoroutine(EndBash());
    }

    IEnumerator EndBash()
    {
        yield return new WaitForSeconds(bashTime);
        rb2d.velocity = Vector2.zero;
    }

    public void SpawnPool()
    {
        if (timeTillPool <= 0)
        {
            // spawns acid pool
            GameObject acidPool = AcidTrailPooling.SharedInstance.GetProjectileObject();
            if (acidPool)
            {
                acidPool.transform.position = new Vector2(acidOffsetX, acidOffsetY) + rb2d.position;
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

    // TODO: don't like how this damage checking is set up, should be standardized in final build
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
