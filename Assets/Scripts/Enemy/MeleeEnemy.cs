using UnityEngine;


public class MeleeEnemy : BaseEnemyClass
{
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;


    [Header("Attack")]
    [Tooltip("The lower the value the faster the enemy fires projectiles")]
    public float AttackRate;
    [Tooltip("The higher the value the further the enemy dashes and the faster the dash is.")]
    public float dashDistance;

    [Header("DEBUG")]
    public float distanceToPlayer1;
    public float distanceToPlayer2;
    public float distanceToTarget;
    public float timeToFire;
    [SerializeField] private GameObject[] players;
    public Transform currentTarget;

    private Rigidbody2D rb2d;
    
    private bool canAttack = true;

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
        Debug.Log("Attacking");
        if (canAttack) {
            rb2d.AddForce((currentTarget.position - transform.position).normalized * dashDistance, ForceMode2D.Impulse);
        } else {
            rb2d.velocity = Vector2.zero;
        }
        canAttack = !canAttack;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Player") && !canAttack && !collider.isTrigger) {
            Debug.Log(collider.name + " has been hit");
        }
    }
}