using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class RangedEnemy : BaseEnemyClass
{
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;
    public float distance;

    [SerializeField] private GameObject projectile;

    private NavMeshAgent navmeshAgent;
    private EnemyNavmesh enemyNavmesh;

    
    private void Awake()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        enemyNavmesh = GetComponent<EnemyNavmesh>();
    }

    private void Start()
    {
        navmeshAgent.speed = moveSpeed;
    }

    private void Update()
    {

    }

    private void Attack()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
