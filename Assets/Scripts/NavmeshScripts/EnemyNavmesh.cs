using UnityEngine.AI;
using UnityEngine;

public class EnemyNavmesh : MonoBehaviour
{
    [SerializeField] private Transform target;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // sets the enemy's destination to the given target transform (player)
    private void Update()
    {
        agent.SetDestination(target.position);
    }
}
