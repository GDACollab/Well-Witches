using UnityEngine;


public class MeleeEnemy : BaseEnemyClass
{
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;


    [Header("Attack")]
    [Tooltip("The lower the value the more the player is hit while in range.")]
    public float attackRate;
    [Tooltip("The higher the value larger the AOE indicated by the red circle")]
    public float attackAOE;

    [Header("DEBUG")]
    public float distanceToPlayer1;
    public float distanceToPlayer2;
    public float distanceToTarget;
    public float timeToFire;
    [SerializeField] private GameObject[] players;
    public Transform currentTarget;

    private bool canAttack = true;

    private void Start()
    {
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
        if (canAttack) 
        {
            if (Vector2.Distance(currentTarget.transform.position, transform.position) < attackAOE) 
            {
                Debug.Log("Deal damage to player");
            }
        }
        canAttack = !canAttack;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Player") && !canAttack && !collider.isTrigger) {
            Debug.Log(collider.name + " has been hit");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackAOE);
    }
}