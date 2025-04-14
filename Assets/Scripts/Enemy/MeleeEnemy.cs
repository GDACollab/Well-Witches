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
    private CircleCollider2D circol;
    
    private bool canAttack = true;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        players = GameObject.FindGameObjectsWithTag("Player");
        circol = GetComponent<CircleCollider2D>();
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
        /*
        A

        */
        Debug.Log("Attacking");
        if (canAttack) {
            // if (circol.IsTouching(players[0].GetComponent<CapsuleCollider2D>())) {
            //     Debug.Log(players[0].name + " has been hit");
            // }
            // if (circol.IsTouching(players[1].GetComponent<CapsuleCollider2D>())) {
            //     Debug.Log(players[1].name + " has been hit");
            // }
            rb2d.AddForce((currentTarget.position - transform.position).normalized * dashDistance, ForceMode2D.Impulse);
        } else {
            rb2d.velocity = Vector2.zero;
        }
        canAttack = !canAttack;
    }

    public void AggroMove() {
        Vector3 direction = currentTarget.position - transform.position;
        rb2d.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Player") && !canAttack && !collider.isTrigger) {
            Debug.Log(collider.name + " has been hit");
        }
    }
}