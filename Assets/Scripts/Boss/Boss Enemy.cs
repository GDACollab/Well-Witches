using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : BaseEnemyClass
{
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;
    [Tooltip("Phase 1 to 2 HP")]
    public float phaseHP;


    [Header("DEBUG")]
    public float distanceToPlayer1;
    public float distanceToPlayer2;
    public float distanceToTarget;
    public float timeToFire;
    [SerializeField] private GameObject[] players;
    public Transform currentTarget;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update()
    {

    }

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

    public void LungeAttack()
    {
        Debug.Log("Lunge Attack");
    }

    public void Shield_Bash()
    {
        Debug.Log("Shield Bash");
    }
    public void Sword_Slash()
    {
        Debug.Log("Sword Slash");
    }
    public void Claw_attack()
    {
        Debug.Log("Claw Attack");
    }
    public void Cape_Swipe()
    {
        Debug.Log("Cape Swipe");
    }
    public void Spawn_Enemies()
    {
        Debug.Log("Spawn Enemies");
    }
}
